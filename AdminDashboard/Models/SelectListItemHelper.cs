using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard
{
    public class SelectListItemHelper
    {
        public static IEnumerable<SelectListItem> GetDelivery_StatusList()
        {
            IList<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem{Text = "Pending", Value = "Pending"},
                new SelectListItem{Text = "start moving", Value = "start moving"},
                new SelectListItem{Text = "The way to you", Value = "The way to you"},
                new SelectListItem{Text = "Delivered", Value = "Delivered"},


            };
            return items;
        }


        public static IEnumerable<SelectListItem> GetGovernmateList()
        {
            IList<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem{Text = "Kalyobiya", Value = "Kalyobiya"},
                new SelectListItem{Text = "Suez", Value = "Suez"},
                new SelectListItem{Text = "Cairo", Value = "Cairo"},
                new SelectListItem{Text = "Alexandria", Value = "Alexandria"},
                new SelectListItem{Text = "Sharqeia", Value = "Sharqeia"},
                new SelectListItem{Text = "Damietta", Value = "Damietta"},
                new SelectListItem{Text = "Kafr El Shiekh", Value = "Kafr El Shiekh"},
                new SelectListItem{Text = "The Red Sea", Value = "The Red Sea"},
                new SelectListItem{Text = "El-Beheira", Value = "El-Beheira"},
                new SelectListItem{Text = "Assiut", Value = "Assiut"},
                new SelectListItem{Text = "New Valley", Value = "New Valley"},
                new SelectListItem{Text = "Qena", Value = "Qena"},
                new SelectListItem{Text = "South Sinai", Value = "South Sinai"},
                new SelectListItem{Text = "Sohag", Value = "Sohag"},
                new SelectListItem{Text = "Fayoum", Value = "Fayoum"},
                new SelectListItem{Text = "Bani Souwaif", Value = "Bani Souwaif"},
                new SelectListItem{Text = "Port-Said", Value = "Port-Said"},
                new SelectListItem{Text = "Matrouh", Value = "Matrouh"},
                new SelectListItem{Text = "Menia", Value = "Menia"},
                new SelectListItem{Text = "Dakahliya", Value = "Dakahliya"},
                new SelectListItem{Text = "Aswan", Value = "Aswan"},
                new SelectListItem{Text = "North Sinai", Value = "North Sinai"},
                new SelectListItem{Text = "Monofiya", Value = "Monofiya"},
                new SelectListItem{Text = "Giza", Value = "Giza"},
                new SelectListItem{Text = "Luxor", Value = "Luxor"},
                new SelectListItem{Text = "Al Gharbya", Value = "Al Gharbya"}

            };
            return items;
        }

    }
}
