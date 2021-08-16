using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModel
{
   public class AprioriForWpf: IComparable<AprioriForWpf>
    {
        public List<string> products { get; set; }
        public List<string> goesWith { get; set; }
        public double confidence { get; set; }
        public AprioriForWpf(List<string> l1, List<string>l2,double conf)
        {
            products = l1;
            goesWith = l2;
            confidence = conf;
        }
        public int sortDes(double conf1, double conf2)
        {

            return conf2.CompareTo(conf1);
        }

        // Default comparer for Part type.
        public int CompareTo(AprioriForWpf comparePart)
        {
            // A null value means that this object is greater.
            if (comparePart == null)
                return 1;

            else
                return comparePart.confidence.CompareTo(this.confidence);
        }
    }
}
