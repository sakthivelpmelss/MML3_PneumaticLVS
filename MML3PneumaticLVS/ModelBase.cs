using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MML3PneumaticLVS
{
    public abstract class ModelBase
    {
		
		private string name;

		public string Name
        {
			get { return name; }
			set { name = value; }
		}
        public string Version { get; set; }

        public  int MAXCYCLE{ get; set; }  
    }
}
