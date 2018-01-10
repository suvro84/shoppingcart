using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class GenericsDemo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        Student dhas = new Student("Manick", "Dhas", 22);
        Student raj = new Student("Sundar", "Raj", 32);

        ///Using a custom strongly typed StudentList
        StudentList mc = new StudentList();
        mc.Add(dhas);
        mc.Add(raj);

        Response.Write("<B><U>Using a custom strongly typed StudentList</B></U><BR>");
        foreach (Student s in mc)
        {
            Response.Write("First Name : " + s.FirstName + "<BR>");
            Response.Write("Last Name : " + s.LastName + "<BR>");
            Response.Write("Age : " + s.Age + "<BR><BR>");
        }

        ///Creating a list of Student objects using my custom generics
        MyCustomList<Student> student = new MyCustomList<Student>();
        student.Add(dhas);
        student.Add(raj);

        Response.Write("<BR><B><U>Using a list of Student objects using my custom generics</B></U><BR>");
        foreach (Student s in student)
        {
            Response.Write("First Name : " + s.FirstName + "<BR>");
            Response.Write("Last Name : " + s.LastName + "<BR>");
            Response.Write("Age : " + s.Age + "<BR><BR>");
        }
        
        ///Creating a list of Student objects using my custom generics
        MyCustomList<int> intlist = new MyCustomList<int>();
        intlist.Add(1);
        intlist.Add(2);

        Response.Write("<BR><B><U>Using a list of String values using my custom generics</B></U><BR>");
        foreach (int i in intlist)
        {
            Response.Write("Index : " + i.ToString() + "<BR>");
        }

        ///Creating a list of Student objects using my custom generics
        MyCustomList<string> strlist = new MyCustomList<string>();
        strlist.Add("One");
        strlist.Add("Two");

        Response.Write("<BR><B><U>Using a list of int values using my custom generics</B></U><BR>");
        foreach (string str in strlist)
        {
            Response.Write("Index : " + str + "<BR>");
        }

    }
}

/// <summary>
/// Student Class
/// </summary>
public class Student
{
    private string fname;
    private string lname;
    private int age;

    /// <summary>
    /// First Name Of The Student
    /// </summary>
    public string FirstName
    {
        get { return fname; }
        set { fname = value; }
    }
    /// <summary>
    /// Last Name Of The Student
    /// </summary>
    public string LastName
    {
        get { return lname; }
        set { lname = value; }
    }
    /// <summary>
    /// Age of The Student
    /// </summary>
    public int Age
    {
        get { return age; }
        set { age = value; }
    }

    /// <summary>
    /// Creates new Instance Of Student
    /// </summary>
    /// <param name="fname">FirstName</param>
    /// <param name="lname">LastName</param>
    /// <param name="age">Age</param>
    public Student(string fname, string lname, int age)
    {
        FirstName = fname;
        LastName = lname;
        Age = age;
    }

}

/// <summary>
/// Strongly Typed Class
/// Accepts only Student Type
/// </summary>
public class StudentList : IEnumerable
{
    private ArrayList alist = new ArrayList();

    /// <summary>
    /// Adds new value of type Student
    /// </summary>
    /// <param name="value">Object of Student Class</param>
    /// <returns>Returns the index of the Object</returns>
    public int Add(Student value)
    {
        try
        {
            return alist.Add(value);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Removes The Object from StudentList
    /// </summary>
    /// <param name="value">Object of Student Class</param>
    public void Remove(Student value)
    {
        try
        {
            alist.Remove(value);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Removes the student object
    /// </summary>
    /// <param name="index">Index of the Student object to be removed</param>
    public void RemoveAt(int index)
    {
        try
        {
            alist.RemoveAt(index);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Gets the count of the StudentList
    /// </summary>
    public int Count
    {
        get { return alist.Count; }
    }

    #region IEnumerable Members
    /// <summary>
    /// Returns an enumerator that iterates through a StudentList.
    /// </summary>
    /// <returns>An System.Collections.IEnumerator object that can be used to iterate through the StudentList</returns>
    public IEnumerator GetEnumerator()
    {
        try
        {
            return alist.GetEnumerator();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion
}


/// <summary>
/// Custom Generic Accepts any Types
/// </summary>
/// <typeparam name="T"></typeparam>
public class MyCustomList<T> : IEnumerable, IEnumerable<T>
{
    private ArrayList alist = new ArrayList();

    /// <summary>
    /// Adds new value of type T
    /// </summary>
    /// <param name="value">Object of Type T</param>
    /// <returns>Returns the index</returns>
    public int Add(T value)
    {
        try
        {
            return alist.Add(value);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Removes The Object from MyCustomList
    /// </summary>
    /// <param name="value">Object of Type T</param>
    public void Remove(T value)
    {
        try
        {
            alist.Remove(value);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Removes the object at index
    /// </summary>
    /// <param name="index">Index of the T object to be removed</param>
    public void RemoveAt(int index)
    {
        try
        {
            alist.RemoveAt(index);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Gets the count of the MyCustomList Object
    /// </summary>
    public int Count
    {
        get { return alist.Count; }
    }

    #region IEnumerable Members

    /// <summary>
    /// Returns an enumerator that iterates through a collection.
    /// </summary>
    /// <returns>An System.Collections.IEnumerator object that can be used to iterate through the collection</returns>
    public IEnumerator GetEnumerator()
    {
        try
        {
            return alist.GetEnumerator();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion

    #region IEnumerable<T> Members

    /// <summary>
    /// Returns an enumerator that iterates through a collection.
    /// </summary>
    /// <returns>An System.Collections.IEnumerator object that can be used to iterate through the collection</returns>
    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        try
        {
            return (IEnumerator<T>)alist.GetEnumerator();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion
}