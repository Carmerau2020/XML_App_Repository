using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;


namespace XML_APP
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateData();
                lbMessage.Text = "Current Database Data";
            }
        }
        private void PopulateData()
        {
            using(XMLDatabaseEntities1 db = new XMLDatabaseEntities1())
            {
                gvData.DataSource = db.EmployeeMaster.ToList();
                gvData.DataBind();
            }

        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (FileUpload1.PostedFile.ContentType == "aplication/xml" || FileUpload1.PostedFile.ContentType == "text/xml")
            {
                string fileName;
                IList<EmployeeMaster> emList;
                try
                {
                    fileName = Path.Combine(Server.MapPath("~/UploadDocuments"), Guid.NewGuid().ToString() + ".xml");
                    FileUpload1.PostedFile.SaveAs(fileName);
                }
                catch(Exception)
                {
                    throw;
                }
                try {
                    XDocument xDoc = XDocument.Load(fileName);
                    emList = xDoc.Descendants("Employee").Select(d =>
                        new EmployeeMaster
                        {
                            EmployeeID = d.Element("EmployeeID").Value,
                            CompanyName = d.Element("CompanyName").Value,
                            ContactName = d.Element("ContactName").Value,
                            ContactTitle = d.Element("ContactTitle").Value,
                            EmployeeAddress = d.Element("EmployeeAddress").Value,
                            PostalCode = d.Element("PostalCode").Value
                        }).ToList();
                    //Update Data
                }
                catch (Exception)
                {
                    throw;
                }
                try
                {

                    using (XMLDatabaseEntities1 db = new XMLDatabaseEntities1())
                    {
                        foreach (var i in emList)
                        {
                            var v = db.EmployeeMaster.Where(a => a.EmployeeID.Equals(i.EmployeeID)).FirstOrDefault();
                            if (v != null)
                            {
                                v.CompanyName = i.CompanyName;
                                v.ContactName = i.ContactName;
                                v.ContactTitle = i.ContactTitle;
                                v.EmployeeAddress = i.EmployeeAddress;
                                v.PostalCode = i.PostalCode;
                            }
                            else
                            {
                                db.EmployeeMaster.Add(i);
                            }
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                try
                {
                    PopulateData();
                    lbMessage.Text = "Import done Successfully";

                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            using(XMLDatabaseEntities1 db = new XMLDatabaseEntities1())
            {
                List<EmployeeMaster> emList = db.EmployeeMaster.ToList();
                if (emList.Count > 0)
                {
                    var xEle = new XElement("Employees",
                        from emp in emList
                        select new XElement("Employee",
                            new XElement("EmployeeID", emp.EmployeeID),
                            new XElement("CompanyName", emp.CompanyName),
                            new XElement("ContactName", emp.ContactName),
                            new XElement("ContactTitle", emp.ContactTitle),
                            new XElement("EmployeeAddress", emp.EmployeeAddress),
                            new XElement("PostalCode", emp.PostalCode)
                            ));
                    HttpContext context = HttpContext.Current;
                    context.Response.Write(xEle);
                    context.Response.ContentType = "aplication/xml";
                    context.Response.AppendHeader("Content-disposition", "atachment; filename=EmployeeData.xml");
                    context.Response.End();
                }
            }
        }
    }
}