using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DxHWebinar.ContextBusService;

namespace DxHWebinar
{
    public partial class BuildWorkflow : System.Web.UI.Page
    {

        const string AdvWorksConnectorId = "AdventureWorks";

        protected void Page_Load(object sender, EventArgs e)
        {
            ContextBusClient cbClient = new ContextBusClient();
            
            //Get Employee definition from adventure works:
            ConnectionParam[] parameters = cbClient.LoadConnection("advworks", AdvWorksConnectorId);
            cbClient.Login(parameters, AdvWorksConnectorId);
            FlyweightObjectDefinition flyweight = new FlyweightObjectDefinition() { Id = "HumanResources.Employee" };
            ObjectDefinition employeeObjDef = cbClient.GetObjectDefinitionList(new FlyweightObjectDefinition[] { flyweight }, AdvWorksConnectorId).First();

            //Get Team definition from Sharepoint
            parameters = cbClient.LoadConnection("Sharepoint-Team", "SharePoint");
            cbClient.Login(parameters, "SharePoint");
            flyweight = new FlyweightObjectDefinition() { Id = "SP|List|/sites/SiteCollection1|Team" };
            ObjectDefinition sharepointObjDef = cbClient.GetObjectDefinitionList(new FlyweightObjectDefinition[] { flyweight }, "SharePoint").First();

            
            //build the workflow.
            TaskManagerClient taskClient = new TaskManagerClient();

            //1. Create login task
            LoginTask login = taskClient.CreateLoginTask(AdvWorksConnectorId, "advworks");

            //2. Create Mapping Task
            MappingTask map = taskClient.CreateMappingTask(sharepointObjDef, employeeObjDef);

            MappableField field = map.TargetFields.SingleOrDefault(f => f.Id == "p.BusinessEntityID");
            field.Mapping.MappingType = MappingType.ConstMapping;
            field.Mapping.Value = 1;

            field = map.TargetFields.SingleOrDefault(f => f.Id == "p.EmailPromotion");
            field.Mapping.MappingType = MappingType.ConstMapping;
            field.Mapping.Value = 0;

            field = map.TargetFields.SingleOrDefault(f => f.Id == "p.Demographics");
            field.Mapping.MappingType = MappingType.ConstMapping;
            field.Mapping.Value = "";

            field = map.TargetFields.SingleOrDefault(f => f.Id == "p.NameStyle");
            field.Mapping.MappingType = MappingType.ConstMapping;
            field.Mapping.Value = false;

            field = map.TargetFields.SingleOrDefault(f => f.Id == "p.PersonType");
            field.Mapping.MappingType = MappingType.ConstMapping;
            field.Mapping.Value = "EM";

            field = map.TargetFields.SingleOrDefault(f => f.Id == "p.FirstName");
            field.Mapping.MappingType = MappingType.FieldMapping;
            field.Mapping.Value = "First_x0020_Name";

            field = map.TargetFields.SingleOrDefault(f => f.Id == "p.LastName");
            field.Mapping.MappingType = MappingType.FieldMapping;
            field.Mapping.Value = "Last_x0020_Name";

            field = map.TargetFields.SingleOrDefault(f => f.Id == "e.JobTitle");
            field.Mapping.MappingType = MappingType.FieldMapping;
            field.Mapping.Value = "Title";

            //3. Save Task
            SaveObjectInstanceTask saveSharepointToEktron = taskClient.CreateSaveObjectTask(AdvWorksConnectorId);

            //4. Logout Task
            LogoutTask logout = taskClient.CreateLogoutTask(AdvWorksConnectorId);

            //5. Assemble and save workflow
            string workflowName = string.Concat("My Test Workflow");
            Workflow workflow = taskClient.CreateWorkflow(workflowName);
            List<ContextBusTask> tasks = new List<ContextBusTask>();
            tasks.Add(login);
            tasks.Add(map);
            tasks.Add(saveSharepointToEktron);
            tasks.Add(logout);
            workflow.Tasks = tasks.ToArray();
            taskClient.SaveWorkflow(workflow);

            //3. Create associated Event Definitions for workflow.
            EventManagerClient eventClient = new EventManagerClient();
            EventDefinition eventDef = new EventDefinition()
            {
                Id = workflowName,
                Payload = map.SourceObject
            };
            eventClient.AssociateWorkflowsToEvent(new string[] { workflow.WorkflowName }, eventDef);


            
            //
            //Now raise the event to perform the workflow!
            //

            //first get sharpeoint item
            ObjectInstanceCriteriaFilter criteria = new ObjectInstanceCriteriaFilter();
            criteria.Filters = new CriteriaFilter[0];
            ObjectInstanceList sharepointObjInstance = cbClient.GetObjectInstanceList(sharepointObjDef, criteria, "SharePoint");

            //create event instance with sharepoint item
            EventInstance itemEvent = new EventInstance()
            {
                Id = workflowName,
                Payload = sharepointObjInstance.Results.First()
            };

            //raise the event.
            eventClient.RaiseEvent(itemEvent);
        }
    }
}