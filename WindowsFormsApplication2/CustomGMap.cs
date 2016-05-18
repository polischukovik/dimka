using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DSS
{
    public partial class CustomGMap : GMap.NET.WindowsForms.GMapControl
    {
       private HashSet<CoordHelper.MapPointer> mPointers;

        public void loadMapPointers()
        {
            mPointers = DBHelper.LoadMapPointers();
        }

        public CustomGMap()
        {
            InitializeComponent();
            Init();
        }

        public CustomGMap(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            Init();
        }

        private void Init()
        {

        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs pe)
        {
            base.OnPaint(pe);

            
        }
    }
}
