using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using LordOfRanger.Arad;

namespace LordOfRanger {
	/// <summary>
	///     npkファイルからスキルアイコンを取得するフォーム
	/// </summary>
	internal partial class SkillIconExtractorForm : Form {

		internal SkillIconExtractorForm() {
			InitializeComponent();
		}

		private async void btnExtract_Click( object sender, EventArgs e ) {
			this.progressBar1.Visible = true;
			this.btnExtract.Enabled = false;
			var dir = this.txtDirectory.Text;
			foreach( var filename in Directory.GetFiles( dir ).Where( x => Regex.IsMatch( x, @"\.npk$", RegexOptions.IgnoreCase ) ).Select( x => x ).ToArray() ) {
				try {
					if( IsFileLocked( filename ) ) {
						MessageBox.Show( "ファイルがロックされています。"+filename );
						this.lblStatus.Text = "";
						this.progressBar1.Visible = false;
						this.btnExtract.Enabled = true;
						return;
					}
					var ext = new Extract( filename );
					foreach( var index in ext.npkIndexList ) {
						if( Regex.IsMatch( index.name, "skillicon" ) ) {
							var filePathNoextern = index.name.Substring( 0, index.name.LastIndexOf( '.' ) ) + Path.DirectorySeparatorChar;
							this.lblStatus.Text = index.name;
							await Task.Run( () => {
								if( !Directory.Exists( filePathNoextern ) ) {
									Directory.CreateDirectory( filePathNoextern );
								}
								foreach( var nImgIndex in index.NImgHeader.NImgIndex ) {
									nImgIndex.NImg.ToBitmap().Save( filePathNoextern + nImgIndex.index + ".png" );
								}
							} );
						}
					}
				} catch( Exception ex ) {
					Console.WriteLine( ex );
				}
			}
			if( Directory.Exists( Path.GetDirectoryName( System.Reflection.Assembly.GetExecutingAssembly().Location ) + Path.DirectorySeparatorChar + "sprite" + Path.DirectorySeparatorChar ) ) {
				Process.Start( Path.GetDirectoryName( System.Reflection.Assembly.GetExecutingAssembly().Location ) + Path.DirectorySeparatorChar + "sprite" + Path.DirectorySeparatorChar );
			} else {
				Process.Start( Path.GetDirectoryName( System.Reflection.Assembly.GetExecutingAssembly().Location ) + Path.DirectorySeparatorChar );
			}
			this.lblStatus.Text = "";
			this.progressBar1.Visible = false;
			this.btnExtract.Enabled = true;
		}

		private void btnBrowse_Click( object sender, EventArgs e ) {
			var fbd = new FolderBrowserDialog {
				ShowNewFolderButton = false,
				SelectedPath = this.txtDirectory.Text
			};
			if( fbd.ShowDialog( this ) == DialogResult.OK ) {
				this.txtDirectory.Text = fbd.SelectedPath;
			}
		}

		private static bool IsFileLocked( string path ) {
			FileStream stream = null;
			try {
				stream = new FileStream( path, FileMode.Open, FileAccess.Read );
			} catch {
				return true;
			} finally {
				stream?.Close();
			}

			return false;
		}
	}
}