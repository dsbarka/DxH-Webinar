using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DxHWebinar.ContextBusService;

namespace DxHWebinar
{
    public partial class ObjectInstances : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ContextBusClient cbClient = new ContextBusClient();
            List<ConnectionParam> paramList = cbClient.LoadConnection("HubSpot - Adam", "HubSpot").ToList();

            cbClient.Login(paramList.ToArray(), "HubSpot");

            
            FlyweightObjectDefinition flyweight = new FlyweightObjectDefinition() { Id = Request.QueryString["id"] };
            FlyweightObjectDefinition[] flyweights = new FlyweightObjectDefinition[] { flyweight };

            ObjectDefinition[] objDefList = cbClient.GetObjectDefinitionList(flyweights, "HubSpot");

            Dictionary<string,object> keys = new Dictionary<string,object>();
            keys.Add("vid", 2);
            ObjectInstance instance = cbClient.GetObjectInstance(objDefList.First(), keys, "HubSpot");

            //ObjectInstanceCriteriaFilter criteria = new ObjectInstanceCriteriaFilter();
            //criteria.Filters = new CriteriaFilter[0];
            //ObjectInstanceList objectInstanceReturn = cbClient.GetObjectInstanceList(objDefList.First(), criteria, "HubSpot");

            //lvObjects.DataSource = objectInstanceReturn.Results;
            lvObjects.DataSource = new ObjectInstance[] { instance };
            lvObjects.DataBind();
        }
    }
}