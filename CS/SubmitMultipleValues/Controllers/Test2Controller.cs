using DevExpress.Web.Mvc;
using SubmitMultipleValues.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SubmitMultipleValues.Controllers
{
    public class Test2Controller : Controller
    {     
        public ActionResult Index()
        {
            return View(new MainModel());
        }
        public ActionResult GridViewPartial()
        {
            return PartialView(BatchEditRepository.GridData);
        }
        public ActionResult BatchUpdatePartial(MVCxGridViewBatchUpdateValues<GridDataItem, int> batchValues)
        {                      
            foreach (var item in batchValues.Insert)
            {
                if (batchValues.IsValid(item))
                    BatchEditRepository.InsertNewItem(item, batchValues);
                else                
                    batchValues.SetErrorText(item, "Correct validation errors");                
            }
            foreach (var item in batchValues.Update)
            {
                if (batchValues.IsValid(item))
                    BatchEditRepository.UpdateItem(item, batchValues);
                else
                    batchValues.SetErrorText(item, "Correct validation errors");
            }
            foreach (var itemKey in batchValues.DeleteKeys)
            {
                BatchEditRepository.DeleteItem(itemKey, batchValues);
            }
            ViewData["ErrorFlag"] = batchValues.EditorErrors.Count > 0;
            return PartialView("GridViewPartial", BatchEditRepository.GridData);
        }
        [HttpPost]
        public ActionResult PostModel(MainModel m){
            if (ModelState.IsValid)
            {
                /*get Batch Edit data from a temp repository and save it to a data base with a main model */
                return View("Success");
            }
            else
                return View("Index");
        }
    }
}
