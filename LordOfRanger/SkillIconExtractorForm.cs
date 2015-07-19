using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;

namespace LordOfRanger {
	/// <summary>
	/// npkファイルからスキルアイコンを取得するフォーム
	/// </summary>
	internal partial class SkillIconExtractorForm : Form {
		internal SkillIconExtractorForm() {
			InitializeComponent();
		}

		private struct NpkHeader {
			internal string flag;
			internal uint count;
		};

		private struct NpkIndex {
			internal uint offset;
			internal uint size;
			internal string name;
		};

		private char[] _decordFlag = ( "puchikon@neople dungeon and fighter DNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNF\0" ).ToCharArray();

		private struct NImgFHeader {
			internal string flag;
			internal uint indexSize;
			internal uint unknown1;
			internal uint unknown2;
			internal uint indexCount;
		};

		private struct NImgFIndex {
			internal uint dwType;
			internal uint dwCompress;
			internal uint width;
			internal uint height;
			internal uint size;
			internal uint keyX;
			internal uint keyY;
			internal uint maxWidth;
			internal uint maxHeight;
		};

		private const uint ARGB_1555 = 0x0e;
		private const uint ARGB_4444 = 0x0f;
		private const uint ARGB_8888 = 0x10;
		private const uint ARGB_NONE = 0x11;

		private const uint COMPRESS_ZLIB = 0x06;
		private const uint COMPRESS_NONE = 0x05;


		private void btnExtract_Click(object sender, EventArgs e) {
			string dir = this.txtDirectory.Text;
			foreach( string filename in Directory.GetFiles( dir ) ) {
				if( System.Text.RegularExpressions.Regex.IsMatch( filename, @"\.npk", System.Text.RegularExpressions.RegexOptions.IgnoreCase ) ) {
					try {
						extract_npk( filename );
					} catch( Exception ) {
						// ignored
					}
				}
			}
			if( Directory.Exists( Application.StartupPath + @"\sprite" ) ) {
				System.Diagnostics.Process.Start( Application.StartupPath + @"\sprite" );
			} else {
				System.Diagnostics.Process.Start( Application.StartupPath );
			}
		}

		private void extract_npk(string file) {
			int offset = 0;
			FileStream fs = new FileStream( file, FileMode.Open, FileAccess.Read );
			byte[] array = new byte[fs.Length];

			fs.Read( array, 0, (int)fs.Length );

			NpkHeader header = new NpkHeader();
			header.flag = Encoding.UTF8.GetString( array, offset, 16 );
			offset += 16;
			header.count = BitConverter.ToUInt32( array, offset );
			offset += 4;

			List<NpkIndex> allFileIndex = new List<NpkIndex>();
			for( int i = 0; i < header.count; ++i ) {
				NpkIndex index = new NpkIndex();
				index.offset = BitConverter.ToUInt32( array, offset );
				offset += 4;
				index.size = BitConverter.ToUInt32( array, offset );
				offset += 4;

				char[] indexName = new char[256];
				for( int j = 0; j < 256; j++ ) {
					indexName[j] = (char)( array[offset++] ^ this._decordFlag[j] );
				}
				index.name = new string( indexName );
				index.name = index.name.Replace( "\0", "" );
				allFileIndex.Add( index );
			}

			foreach( NpkIndex index in allFileIndex ) {
				if( System.Text.RegularExpressions.Regex.IsMatch( index.name, "skillicon" ) ) {
					extract_img_npk( array, (int)index.offset, index.name );
				}
			}
		}
		private void extract_img_npk(byte[] array, int indexOffset, string indexName) {

			int offset = indexOffset;
			string filePathNoextern = indexName.Substring( 0, indexName.LastIndexOf( '.' ) );
			NImgFHeader header = new NImgFHeader();
			header.flag = Encoding.UTF8.GetString( array, offset, 16 );
			offset += 16;
			header.indexSize = BitConverter.ToUInt32( array, offset );
			offset += 4;

			header.unknown1 = BitConverter.ToUInt32( array, offset );
			offset += 4;
			header.unknown2 = BitConverter.ToUInt32( array, offset );
			offset += 4;
			header.indexCount = BitConverter.ToUInt32( array, offset );
			offset += 4;

			if( header.flag.IndexOf( "Neople Img File", StringComparison.Ordinal ) != 0 ) {
				Console.WriteLine( "error flag " + header.flag + " in file " + indexName );
				return;
			}

			List<NImgFIndex> allFileIndex = new List<NImgFIndex>();
			for( int i = 0; i < header.indexCount; ++i ) {
				NImgFIndex index = new NImgFIndex();

				index.dwType = BitConverter.ToUInt32( array, offset );
				offset += 4;
				index.dwCompress = BitConverter.ToUInt32( array, offset );
				offset += 4;

				if( index.dwType == ARGB_NONE ) {
					allFileIndex.Add( index );
					continue;
				}


				index.width = BitConverter.ToUInt32( array, offset );
				offset += 4;
				index.height = BitConverter.ToUInt32( array, offset );
				offset += 4;
				index.size = BitConverter.ToUInt32( array, offset );
				offset += 4;
				index.keyX = BitConverter.ToUInt32( array, offset );
				offset += 4;
				index.keyY = BitConverter.ToUInt32( array, offset );
				offset += 4;
				index.maxWidth = BitConverter.ToUInt32( array, offset );
				offset += 4;
				index.maxHeight = BitConverter.ToUInt32( array, offset );
				offset += 4;

				allFileIndex.Add( index );
			}


			const int bufferSize = 1024 * 1024 * 3;
			byte[] tempFileData = new byte[bufferSize];
			byte[] tempZlibData = new byte[bufferSize];
			offset = indexOffset + (int)header.indexSize + 32;

			int count = 0;
			foreach( NImgFIndex index in allFileIndex ) {
				if( index.dwType == ARGB_NONE ) {
					continue;
				}

				uint size = index.size;

				if( index.dwCompress == COMPRESS_NONE ) {
					if( index.dwType == ARGB_8888 ) {
						size = index.size;
					} else if( index.dwType == ARGB_1555 || index.dwType == ARGB_4444 ) {
						size = index.size / 2;
					}
				}

				if( index.dwCompress == COMPRESS_ZLIB ) {

					MemoryStream ms = new MemoryStream( array, offset + 2, (int)size - 2 );
					DeflateStream ds = new DeflateStream( ms, CompressionMode.Decompress );
					try {

						ds.Read( tempFileData, 0, tempFileData.Length );
					} catch( Exception ) {
						Console.WriteLine( "compress " + indexName + "error!" );
						offset += (int)size;
						continue;
					}
					ms.Close();
					ds.Close();
				} else if( index.dwCompress == COMPRESS_NONE ) {
					MemoryStream ms = new MemoryStream( array, offset + 2, (int)size );
					ms.Read( tempFileData, 0, tempFileData.Length );
					ms.Close();
				} else {
					Console.WriteLine( "error unknown compress type: " + index.dwCompress + " in file " + indexName );
				}

				Directory.CreateDirectory( filePathNoextern );


				string filename = Path.GetFileName( filePathNoextern );
				convert_to_png( filePathNoextern + "/" + filename + "_" + count++ + ".png", (int)index.width, (int)index.height, index.dwType, tempFileData );
				offset += (int)size;
			}
		}

		private void convert_to_png(string fileName, int width, int height, uint type, byte[] data) {
			Bitmap bmp = new Bitmap( width, height );
			for( int i = 0; i < height; i++ ) {
				for( int j = 0; j < width; ++j ) {
					switch( type ) {
						case ARGB_1555:
							{
								int r = ( ( ( data[i * width * 2 + j * 2 + 1] & 127 ) >> 2 ) << 3 ) % 256;   // red
								int g = ( ( ( ( data[i * width * 2 + j * 2 + 1] & 0x0003 ) << 3 ) | ( ( data[i * width * 2 + j * 2] >> 5 ) & 0x0007 ) ) << 3 ) % 256;   // green
								int b = ( ( data[i * width * 2 + j * 2] & 0x003f ) << 3 ) % 256;   // blue
								int a = ( ( data[i * width * 2 + j * 2 + 1] >> 7 ) ) % 256 == 0 ? 0 : 255;  // alpha

								bmp.SetPixel( j, i, Color.FromArgb( a, r, g, b ) );
							}
							break;
						case ARGB_4444:
							{

								int r = ( ( data[i * width * 2 + j * 2 + 1] & 0x0f ) << 4 ) % 256;    // red
								int g = ( ( ( data[i * width * 2 + j * 2 + 0] & 0xf0 ) >> 4 ) << 4 ) % 256; // green
								int b = ( ( data[i * width * 2 + j * 2 + 0] & 0x0f ) << 4 ) % 256;  // blue
								int a = ( ( data[i * width * 2 + j * 2 + 1] & 0xf0 ) >> 4 ) << 4;   // alpha

								bmp.SetPixel( j, i, Color.FromArgb( a, r, g, b ) );
							}
							break;
						case ARGB_8888:
							{

								int r = data[i * width * 4 + j * 4 + 2];  // red
								int g = data[i * width * 4 + j * 4 + 1];  // green
								int b = data[i * width * 4 + j * 4 + 0];  // blue
								int a = data[i * width * 4 + j * 4 + 3];  // alpha

								bmp.SetPixel( j, i, Color.FromArgb( a, r, g, b ) );
							}
							break;
						case ARGB_NONE:
							break;
						default:
							Console.WriteLine( "error known type:" + type );
							break;
					}
				}
			}

			bmp.Save( fileName, System.Drawing.Imaging.ImageFormat.Png );

		}

		private void btnBrowse_Click(object sender, EventArgs e) {
			FolderBrowserDialog fbd = new FolderBrowserDialog();
			fbd.ShowNewFolderButton = false;
			fbd.SelectedPath = this.txtDirectory.Text;
			if( fbd.ShowDialog( this ) == DialogResult.OK ) {
				this.txtDirectory.Text = fbd.SelectedPath;
			}
		}
	}
}
