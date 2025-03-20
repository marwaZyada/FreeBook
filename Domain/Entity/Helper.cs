using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
   public class Helper
    {
        public const string PathImageUser = "Images/Users";
        public const string DeleteImageUser = "Images\\Users";

        public enum EcurrentState {
        Active=1,
        Delete=0
        }
    }
}
