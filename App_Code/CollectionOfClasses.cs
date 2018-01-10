using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Collections.ObjectModel;

/// <summary>
/// Summary description for CollectionOfClasses
/// </summary>
public class CollectionOfClasses
{
    public CollectionOfClasses()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public class GCommon<T> : Collection<T> where T : icommonInterface
    {

        public void Add(T item)
        {
            base.Add(item);
        }
        public void Clear(T item)
        {
            base.Clear();
        }
    }



    public interface icommonInterface
    {
    }
    public class student : icommonInterface
    {
        private string _name;
        private int _rollno;
        public string name
        {
            get
            {
                return _name;
            }
            set
            {

                _name = value;
            }
        }

        public int rollno
        {
            get
            {
                return _rollno;
            }
            set
            {

                _rollno = value;
            }
        }

    }
}
