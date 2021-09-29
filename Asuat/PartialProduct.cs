using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asuat
{
    public partial class Product
    {
        public string TimeRent
        {
            get
            {
                string [] StDate = Convert.ToString(StartRent).Split(new char[] { ' ' });
                string [] EnDate = Convert.ToString(EndRent).Split(new char[] { ' ' });

                return ($"{StDate[0]} - {EnDate[0]}");
            }
        }
        public string Pledge
        {
            get
            {
                return ($"{PledgePrice} руб.");
            }
        }
        public string Priсe
        {
            get
            {
                return ($"{PriceProduct} руб/день");
            }
        }
        public string Rentt
        {
            get
            {
                if (Rent == false)
                {
                    return "Не арендован";
                }
                else
                {
                    return "Арендован";
                }

            }
        }
    }
}
