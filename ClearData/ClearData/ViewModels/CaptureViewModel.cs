using System;
using System.Collections.Generic;
using System.Text;

namespace ClearData.ViewModels
{
    public class CaptureViewModel
    {
        public string NameText
        {
            get
            {
                return UserInfo.name;
            }
            set
            {
                UserInfo.name = value;
            }
        }

        public DateTime DateVal
        {
            get
            {
                return UserInfo.DOB;
            }
            set
            {
                UserInfo.DOB = value;
            }
        }

        public string PlaceText
        {
            get
            {
                return UserInfo.birthPlace;
            }
            set
            {
                UserInfo.birthPlace = value;
            }
        }
    }
}
