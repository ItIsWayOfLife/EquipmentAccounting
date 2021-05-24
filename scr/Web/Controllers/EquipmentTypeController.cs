using AutoMapper;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Web.Models.EquipmentType;
using Web.Models.Position;

namespace Web.Controllers
{
    public class EquipmentTypeController : Controller
    {
        private readonly IEquipmentTypeService _equipmentTypeService;

        public EquipmentTypeController(IEquipmentTypeService equipmentTypeService)
        {
            _equipmentTypeService = equipmentTypeService;
        }

        [HttpGet]
        public IActionResult Index(string searchSelectionString, string searchString)
        {
            var equipmentTypes = _equipmentTypeService.GetAll();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EquipmentType, EquipmentTypeViewModel>()).CreateMapper();
            var equipmentTypesViewModels = mapper.Map<IEnumerable<EquipmentType>, List<EquipmentTypeViewModel>>(equipmentTypes);

            // list search
            List<string> searchSelection = new List<string>() { "Поиск", "Id", "Названию" };

            // search
            if (searchSelectionString != string.Empty && searchSelectionString != null && searchSelectionString != "Поиск" && searchString != null)
            {
                if (searchSelection[1].ToLower() == searchSelectionString.ToLower() && searchString != string.Empty)
                {
                    equipmentTypesViewModels = equipmentTypesViewModels.Where(p => p.Id.ToString() != null && p.Id.ToString().ToLower().Equals(searchString.ToLower())).ToList();
                }
                else if (searchSelection[1].ToLower() == searchSelectionString.ToLower() && searchString == string.Empty)
                {
                    equipmentTypesViewModels = equipmentTypesViewModels.Where(p => p.Id.ToString() == null || p.Id.ToString() == string.Empty).ToList();
                }
                else if (searchSelection[2].ToLower() == searchSelectionString.ToLower() && searchString != string.Empty)
                {
                    equipmentTypesViewModels = equipmentTypesViewModels.Where(p => p.Name != null && p.Name.ToLower().Contains(searchString.ToLower())).ToList();
                }
                else if (searchSelection[2].ToLower() == searchSelectionString.ToLower() && searchString == string.Empty)
                {
                    equipmentTypesViewModels = equipmentTypesViewModels.Where(p => p.Name == null || p.Name == string.Empty).ToList();
                }
            }

            return View(new EquipmentTypeIndexViewModel()
            {
                 EquipmentTypeViewModel = equipmentTypesViewModels,
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
        public IActionResult Add(PositionViewModel model, string searchSelectionString, string searchString)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;

            if (ModelState.IsValid)
            {
                var equipmentType = new EquipmentType()
                {
                    Id = model.Id,
                    Name = model.Name
                };

                try
                {
                    _equipmentTypeService.Add(equipmentType);

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
                _equipmentTypeService.Delete(id);
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

            var equipmentType = _equipmentTypeService.Get(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EquipmentType, EquipmentTypeViewModel>()).CreateMapper();
            var equipmentTypeViewModel = mapper.Map<EquipmentType, EquipmentTypeViewModel>(equipmentType);

            return View(equipmentTypeViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EquipmentTypeViewModel model, string searchSelectionString, string searchString)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;

            if (ModelState.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EquipmentTypeViewModel, EquipmentType>()).CreateMapper();
                var equipmentType = mapper.Map<EquipmentTypeViewModel, EquipmentType>(model);

                try
                {
                    _equipmentTypeService.Edit(equipmentType);

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
