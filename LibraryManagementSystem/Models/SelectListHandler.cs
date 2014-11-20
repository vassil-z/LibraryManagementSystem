using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using LibraryManagementSystem.DataAccess.Entities;

namespace LibraryManagementSystem.Models
{
    public class SelectListHandler
    {
        public static List<SelectListItem> Create<T>(
            List<T> items, Func<T, string> getText,
            Func<T, string> getValue, string selectedValue = null)
            where T : BaseEntityWithID
        {
            var result = new List<SelectListItem>();
            foreach (var item in items)
            {
                result.Add(new SelectListItem()
                    {
                        Text  = getText(item),
                        Value = getValue(item),
                        Selected = selectedValue != null && selectedValue == getText(item)
                    });
            }

            return result.OrderBy(item => item.Text).ToList();
        }

    }
}