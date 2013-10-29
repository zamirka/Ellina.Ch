using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testProject.Models;

namespace testProject.Infrastructure.Search
{
    public class SearchUserByNameComparer : IComparer<User>
    {
        private readonly string _name;

        public SearchUserByNameComparer(string name)
        {
            _name = name.ToLower();
        }

        int IComparer<User>.Compare(User x, User y)
        {
            var xIndex = x.name.ToLower().IndexOf(_name);
            var yIndex = y.name.ToLower().IndexOf(_name);
            if (xIndex < yIndex)
            {
                return -1;
            }
            else if (xIndex > yIndex)
            {
                return 1;
            }

            return 0;
        }
    }
}