using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;


public partial class EditCategory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            PopulateRootLevel();
    }


    private void PopulateRootLevel()
    {
        SqlConnection objConn = new SqlConnection(@"initial catalog=employee;Integrated Security=true;Data Source=PERSONAL-AF5588\SQLEXPRESS");

        //SqlCommand objCommand = new SqlCommand(@"select id,title,(select count(*) FROM SampleCategories WHERE parentid=sc.id) childnodecount FROM SampleCategories sc where parentID IS NULL", objConn);
        //SqlCommand objCommand = new SqlCommand(@"select Category_Id,Category_Name,(select count(*) FROM tblItemCategory_Web_Server WHERE Parent_Category_id=Category_Id) childnodecount FROM tblItemCategory_Web_Server  where Parent_Category_id =0", objConn);
        SqlCommand objCommand = new SqlCommand(@"select Category_Id,Category_Name,(select count(*) FROM tblItemCategory_Web_Server WHERE Parent_Category_id=sc.Category_Id) childnodecount FROM tblItemCategory_Web_Server sc where Parent_Category_id =0", objConn);

        
        SqlDataAdapter da = new SqlDataAdapter(objCommand);
        DataTable dt = new DataTable();
        da.Fill(dt);
        PopulateNodes(dt, TreeView1.Nodes);
    }

  
    private void PopulateNodes(DataTable dt, TreeNodeCollection nodes)
    {
        foreach (DataRow dr in dt.Rows)
        {
            TreeNode tn = new TreeNode();
            tn.Text = dr["Category_Name"].ToString();
            tn.Value = dr["Category_Id"].ToString();
            tn.NavigateUrl = "#";
            tn.ShowCheckBox=true;
            nodes.Add(tn);

            //If node has child nodes, then enable on-demand populating
           tn.PopulateOnDemand = ((int)(dr["childnodecount"]) > 0);
        }
    }

    private void PopulateSubLevel(int parentid, TreeNode parentNode)
    {
        //SqlConnection objConn = new SqlConnection(@"server=JOTEKE\SQLExpress;Trusted_Connection=true;DATABASE=TreeViewSampleDB");
        SqlConnection objConn = new SqlConnection(@"initial catalog=employee;Integrated Security=true;Data Source=PERSONAL-AF5588\SQLEXPRESS");

        //SqlCommand objCommand = new SqlCommand(@"select id,title,(select count(*) FROM SampleCategories WHERE parentid=sc.id) childnodecount FROM SampleCategories sc where parentID=@parentID", objConn);
        SqlCommand objCommand = new SqlCommand(@"select Category_Id,Category_Name,(select count(*) FROM tblItemCategory_Web_Server WHERE Parent_Category_id=sc.Category_Id) childnodecount  FROM tblItemCategory_Web_Server sc where  Parent_Category_id=@Parent_Category_id", objConn);

        objCommand.Parameters.Add("@Parent_Category_id", SqlDbType.Int).Value = parentid;
        SqlDataAdapter da = new SqlDataAdapter(objCommand);
        DataTable dt = new DataTable();
        da.Fill(dt);
        PopulateNodes(dt, parentNode.ChildNodes);
    }
    protected void TreeView1_TreeNodePopulate1(object sender, TreeNodeEventArgs e)
    {
        PopulateSubLevel(Int32.Parse(e.Node.Value), e.Node);
    }
    protected void brnAdd_Click(object sender, EventArgs e)
    {
        if (TreeView1.CheckedNodes.Count > 0)
        {


            foreach (TreeNode tnode in TreeView1.CheckedNodes)
            {
                if (tnode.Checked)
                {
                    string strTreeValue = tnode.Value;
                    string strTreetext = tnode.Text;
                }
            }
        }
    }
    protected void btnCheckAll_Click(object sender, EventArgs e)
    {
        foreach (TreeNode tn in TreeView1.Nodes)
        {
            tn.Checked = true;
            if (tn.ChildNodes.Count > 0)
            {
                foreach (TreeNode tchild in tn.ChildNodes)
                {
                    if (tchild.Checked == false)
                    {
                        tchild.Checked = true;
                    }
                }
            }
        }
      

       

    }
}
