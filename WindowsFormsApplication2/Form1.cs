using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSS
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        
            // Initialize map:
            
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            //gmap.SetPositionByKeywords("Kiev, Ukraine");
            gmap.Position = new GMap.NET.PointLatLng(49.872059, 30.096184);
            gmap.MinZoom = 12;
            gmap.MaxZoom = 16;
            gmap.Zoom = 13;
            //gmap.MapProvider = GMap.NET.MapProviders.GoogleSatelliteMapProvider.Instance;

            comboBox1.ValueMember = "Name";
            comboBox1.DataSource = GMap.NET.MapProviders.GMapProviders.List;
            comboBox1.SelectedItem = GMap.NET.MapProviders.YandexSatelliteMapProvider.Instance;

            trackBar1.Value = Convert.ToInt32(gmap.Zoom);
            label2.Text = trackBar1.Value.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gmap.MapProvider = comboBox1.SelectedItem as GMap.NET.MapProviders.GMapProvider;
                gmap.Position = new GMap.NET.PointLatLng(49.854088, 30.116234);
            }
            catch (Exception)
            {
                ;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            gmap.Zoom = trackBar1.Value;
            label2.Text = trackBar1.Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MapTools.LoadMapPhotosFromFolder());
        }
    }
}
