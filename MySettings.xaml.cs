using System.Windows;

using Ookii;
using Ookii.Dialogs.Wpf;

namespace MeineSammlungen_3
{
    /// <summary>
    /// Interaktionslogik für MySettings.xaml
    /// </summary>
    public partial class MySettings : Window
    {
        DataClassesSammlungenDataContext con = new DataClassesSammlungenDataContext();
        public MySettings()
        {
            InitializeComponent();
            this.DataContext = con.Settings;
        }

        private void btnFolder_Click(object sender, RoutedEventArgs e)
            //ookii-wpf-Dialog genutzt, da Windows.Forms nicht verwendbar ist!
        {
            VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();
            dialog.Description = "Please select a folder.";
            dialog.UseDescriptionForTitle = true; // This applies to the Vista style dialog only, not the old dialog.
            if (!VistaFolderBrowserDialog.IsVistaFolderDialogSupported)
                MessageBox.Show(this, "Because you are not using Windows Vista or later, the regular folder browser dialog will be used. Please use Windows Vista to see the new dialog.", "Sample folder browser dialog");
            if ((bool)dialog.ShowDialog(this))
            txtImgPath.Text = dialog.SelectedPath;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            con.SubmitChanges();
            DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
