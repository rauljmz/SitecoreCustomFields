using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Shell.Applications.ContentEditor;

namespace CustomFields.Controls
{
    public class CustomTreeList : TreeList
    {
        private static string QueryPrefix = "query:";
        public override string DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                base.DataSource = CheckForQuery(value);
            }
        }
        protected virtual string CheckForQuery(string value)
        {
            if (value.StartsWith(QueryPrefix))
            {
                var item = Sitecore.Context.ContentDatabase.GetItem(this.ItemID);
                if (item != null)
                {
                    var root = item.Axes.SelectSingleItem(value.Substring(QueryPrefix.Length));
                    if (root != null)
                    {
                        return root.ID.ToString();
                    }
                }
            }
            return value;
        }
    }
}
