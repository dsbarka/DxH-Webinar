using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DxHWebinar.ContextBusService;

namespace DxHWebinar
{
    public partial class ObjectDef : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            ContextBusClient cbClient = new ContextBusClient();
            List<ConnectionParam> paramList = cbClient.LoadConnection("HubSpot - Adam", "HubSpot").ToList();

            cbClient.Login(paramList.ToArray(), "HubSpot");

            FlyweightObjectDefinition flyweight = new FlyweightObjectDefinition() { Id = Request.QueryString["id"] };
            FlyweightObjectDefinition[] flyweights = new FlyweightObjectDefinition[] { flyweight };

            ObjectDefinition[] objDefList = cbClient.GetObjectDefinitionList(flyweights, "HubSpot");

            lvFields.DataSource = objDefList.First().Fields;
            lvFields.DataBind();


        }
    }
}