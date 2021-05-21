using AutoMapper;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Web.Models.StatusEquipment;

namespace Web.Controllers
{
    public class StatusEquipmentController : Controller
    {
        private readonly IStatusEquipmentService _statusEquipmentService;

        public StatusEquipmentController(IStatusEquipmentService statusEquipmentService)
        {
            _statusEquipmentService = statusEquipmentService;
        }

        [HttpGet]
        public IActionResult Index(string searchSelectionString, string searchString)
        {
            var statusEquipments = _statusEquipmentService.GetAll();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<StatusEquipment, StatusEquipmentViewModel>()).CreateMapper();
            var statusEquipmentsViewModels = mapper.Map<IEnumerable<StatusEquipment>, List<StatusEquipmentViewModel>>(statusEquipments);

            // list search
            List<string> searchSelection = new List<string>() { "Поиск", "Id", "Названию" };

            // search
            if (searchSelectionString != string.Empty && searchSelectionString != null && searchSelectionString != "Поиск" && searchString != null)
            {
                if (searchSelection[1].ToLower() == searchSelectionString.ToLower() && searchString != string.Empty)
                {
                    statusEquipmentsViewModels = statusEquipmentsViewModels.Where(p => p.Id.ToString() != null && p.Id.ToString().ToLower().Equals(searchString.ToLower())).ToList();
                }
                else if (searchSelection[1].ToLower() == searchSelectionString.ToLower() && searchString == string.Empty)
                {
                    statusEquipmentsViewModels = statusEquipmentsViewModels.Where(p => p.Id.ToString() == null || p.Id.ToString() == string.Empty).ToList();
                }
                else if (searchSelection[2].ToLower() == searchSelectionString.ToLower() && searchString != string.Empty)
                {
                    statusEquipmentsViewModels = statusEquipmentsViewModels.Where(p => p.Name != null && p.Name.ToLower().Contains(searchString.ToLower())).ToList();
                }
                else if (searchSelection[2].ToLower() == searchSelectionString.ToLower() && searchString == string.Empty)
                {
                    statusEquipmentsViewModels = statusEquipmentsViewModels.Where(p => p.Name == null || p.Name == string.Empty).ToList();
                }
            }

            return View(new StatusEquipmentIndexViewModel()
            {
                StatusEquipmentViewModels = statusEquipmentsViewModels,
                SearchSelection = new SelectList(searchSelection),
                SearchSelectionString = searchSelectionString,
                SearchString = searchString
            });
        }

        [HttpGet]
        public IActionResult Add(string searchSelectionString, string searchString)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;

            return View();
        }

        [HttpPost]
        public IActionResult Add(StatusEquipmentViewModel model, string searchSelectionString, string searchString)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;

            if (ModelState.IsValid)
            {
                var statusEquipment = new StatusEquipment()
                {
                    Id = model.Id,
                    Name = model.Name
                };

                try
                {
                    _statusEquipmentService.Add(statusEquipment);

                    return RedirectToAction("Index", new { searchSelectionString, searchString });
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id, string searchSelectionString, string searchString)
        {
            try
            {
                _statusEquipmentService.Delete(id);
            }
            catch (ValidationException)
            {
                return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = string.Empty });
            }

            return RedirectToAction("Index", new { searchSelectionString, searchString });
        }

        [HttpGet]
        public IActionResult Edit(int id, string searchSelectionString, string searchString)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;

            var statusEquipment = _statusEquipmentService.Get(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<StatusEquipment, StatusEquipmentViewModel>()).CreateMapper();
            var statusEquipmentViewModel = mapper.Map<StatusEquipment, StatusEquipmentViewModel>(statusEquipment);

            return View(statusEquipmentViewModel);
        }

        [HttpPost]
        public IActionResult Edit(StatusEquipmentViewModel model, string searchSelectionString, string searchString)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;

            if (ModelState.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<StatusEquipmentViewModel, StatusEquipment>()).CreateMapper();
                var statusEquipment = mapper.Map<StatusEquipmentViewModel, StatusEquipment>(model);

                try
                {
                    _statusEquipmentService.Edit(statusEquipment);

                    return RedirectToAction("Index", new { searchSelectionString, searchString });
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            }

            return View(model);
        }
    
    }
}
