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

		private struct NPK_Header {
			internal string flag;
			internal uint count;
		};

		private struct NPK_Index {
			internal uint offset;
			internal uint size;
			internal string name;
		};

		private char[] decord_flag = ( "puchikon@neople dungeon and fighter DNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNF\0" ).ToCharArray();

		private struct NImgF_Header {
			internal string flag;
			internal uint index_size;
			internal uint unknown1;
			internal uint unknown2;
			internal uint index_count;
		};

		private struct NImgF_Index {
			internal uint dwType;
			internal uint dwCompress;
			internal uint width;
			internal uint height;
			internal uint size;
			internal uint key_x;
			internal uint key_y;
			internal uint max_width;
			internal uint max_height;
		};

		private const uint ARGB_1555 = 0x0e;
		private const uint ARGB_4444 = 0x0f;
		private const uint ARGB_8888 = 0x10;
		private const uint ARGB_NONE = 0x11;

		private const uint COMPRESS_ZLIB = 0x06;
		private const uint COMPRESS_NONE = 0x05;


		private void btnExtract_Click(object sender, EventArgs e) {
			string dir = txtDirectory.Text;
			foreach( string filename in Directory.GetFiles( dir ) ) {
				if( System.Text.RegularExpressions.Regex.IsMatch( filename, @"\.npk", System.Text.RegularExpressions.RegexOptions.IgnoreCase ) ) {
					try {
						extract_npk( filename );
					} catch( Exception ) {

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

			NPK_Header header = new NPK_Header();
			header.flag = Encoding.UTF8.GetString( array, offset, 16 );
			offset += 16;
			header.count = BitConverter.ToUInt32( array, offset );
			offset += 4;

			List<NPK_Index> all_file_index = new List<NPK_Index>();
			for( int i = 0; i < header.count; ++i ) {
				NPK_Index index = new NPK_Index();
				index.offset = BitConverter.ToUInt32( array, offset );
				offset += 4;
				index.size = BitConverter.ToUInt32( array, offset );
				offset += 4;

				char[] index_name = new char[256];
				for( int j = 0; j < 256; j++ ) {
					index_name[j] = (char)( array[offset++] ^ decord_flag[j] );
				}
				index.name = new string( index_name );
				index.name = index.name.Replace( "\0", "" );
				all_file_index.Add( index );
			}

			foreach( NPK_Index index in all_file_index ) {
				if( System.Text.RegularExpressions.Regex.IsMatch( index.name, "skillicon" ) ) {
					extract_img_npk( array, (int)index.offset, index.name );
				}
			}
		}
		private void extract_img_npk(byte[] array, int index_offset, string index_name) {

			int offset = index_offset;
			string file_path_noextern = index_name.Substring( 0, index_name.LastIndexOf( '.' ) );
			NImgF_Header header = new NImgF_Header();
			header.flag = Encoding.UTF8.GetString( array, offset, 16 );
			offset += 16;
			header.index_size = BitConverter.ToUInt32( array, offset );
			offset += 4;

			header.unknown1 = BitConverter.ToUInt32( array, offset );
			offset += 4;
			header.unknown2 = BitConverter.ToUInt32( array, offset );
			offset += 4;
			header.index_count = BitConverter.ToUInt32( array, offset );
			offset += 4;

			if( header.flag.IndexOf( "Neople Img File" ) != 0 ) {
				Console.WriteLine( "error flag " + header.flag + " in file " + index_name );
				return;
			}

			List<NImgF_Index> all_file_index = new List<NImgF_Index>();
			for( int i = 0; i < header.index_count; ++i ) {
				NImgF_Index index = new NImgF_Index();

				index.dwType = BitConverter.ToUInt32( array, offset );
				offset += 4;
				index.dwCompress = BitConverter.ToUInt32( array, offset );
				offset += 4;

				if( index.dwType == ARGB_NONE ) {
					all_file_index.Add( index );
					continue;
				}


				index.width = BitConverter.ToUInt32( array, offset );
				offset += 4;
				index.height = BitConverter.ToUInt32( array, offset );
				offset += 4;
				index.size = BitConverter.ToUInt32( array, offset );
				offset += 4;
				index.key_x = BitConverter.ToUInt32( array, offset );
				offset += 4;
				index.key_y = BitConverter.ToUInt32( array, offset );
				offset += 4;
				index.max_width = BitConverter.ToUInt32( array, offset );
				offset += 4;
				index.max_height = BitConverter.ToUInt32( array, offset );
				offset += 4;

				all_file_index.Add( index );
			}


			const int buffer_size = 1024 * 1024 * 3;
			byte[] temp_file_data = new byte[buffer_size];
			byte[] temp_zlib_data = new byte[buffer_size];
			offset = index_offset + (int)header.index_size + 32;

			int count = 0;
			foreach( NImgF_Index index in all_file_index ) {
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

						int readBytes = ds.Read( temp_file_data, 0, temp_file_data.Length );
					} catch( Exception e ) {
						Console.WriteLine( "compress " + index_name + "error!" );
						offset += (int)size;
						continue;
					}
					ms.Close();
					ds.Close();
				} else if( index.dwCompress == COMPRESS_NONE ) {
					MemoryStream ms = new MemoryStream( array, offset + 2, (int)size );
					ms.Read( temp_file_data, 0, temp_file_data.Length );
					ms.Close();
				} else {
					Console.WriteLine( "error unknown compress type: " + index.dwCompress + " in file " + index_name );
				}

				Directory.CreateDirectory( file_path_noextern );


				string filename = Path.GetFileName( file_path_noextern );
				convert_to_png( file_path_noextern + "/" + filename + "_" + count++ + ".png", (int)index.width, (int)index.height, index.dwType, temp_file_data );
				offset += (int)size;
			}
		}

		private void convert_to_png(string file_name, int width, int height, uint type, byte[] data) {
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
							Console.WriteLine( "error known type:%d\n", type );
							break;
					}
				}
			}

			bmp.Save( file_name, System.Drawing.Imaging.ImageFormat.Png );

		}

		private void btnBrowse_Click(object sender, EventArgs e) {
			FolderBrowserDialog fbd = new FolderBrowserDialog();
			fbd.ShowNewFolderButton = false;
			fbd.SelectedPath = txtDirectory.Text;
			if( fbd.ShowDialog( this ) == DialogResult.OK ) {
				txtDirectory.Text = fbd.SelectedPath;
			}
		}
	}
}
