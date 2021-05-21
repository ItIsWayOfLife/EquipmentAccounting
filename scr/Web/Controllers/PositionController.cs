using AutoMapper;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Web.Models.Position;

namespace Web.Controllers
{
    public class PositionController : Controller
    {
        private readonly IPositionService _positionService;

        public PositionController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpGet]
        public IActionResult Index(string searchSelectionString, string searchString)
        {
            var positions = _positionService.GetAll();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Position, PositionViewModel>()).CreateMapper();
            var positionsViewModels = mapper.Map<IEnumerable<Position>, List<PositionViewModel>>(positions);

            // list search
            List<string> searchSelection = new List<string>() { "Поиск", "Id", "Названию" };

            // search
            if (searchSelectionString != string.Empty && searchSelectionString != null && searchSelectionString != "Поиск" && searchString != null)
            {
                if (searchSelection[1].ToLower() == searchSelectionString.ToLower() && searchString != string.Empty)
                {
                    positionsViewModels = positionsViewModels.Where(p => p.Id.ToString() != null && p.Id.ToString().ToLower().Equals(searchString.ToLower())).ToList();
                }
                else if (searchSelection[1].ToLower() == searchSelectionString.ToLower() && searchString == string.Empty)
                {
                    positionsViewModels = positionsViewModels.Where(p => p.Id.ToString() == null || p.Id.ToString() == string.Empty).ToList();
                }
                else if (searchSelection[2].ToLower() == searchSelectionString.ToLower() && searchString != string.Empty)
                {
                    positionsViewModels = positionsViewModels.Where(p => p.Name != null && p.Name.ToLower().Contains(searchString.ToLower())).ToList();
                }
                else if (searchSelection[2].ToLower() == searchSelectionString.ToLower() && searchString == string.Empty)
                {
                    positionsViewModels = positionsViewModels.Where(p => p.Name == null || p.Name == string.Empty).ToList();
                }
            }

            return View(new PositionIndexViewModel()
            {
                PositionViewModels = positionsViewModels,
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
                var position = new Position()
                {
                    Id = model.Id,
                    Name = model.Name
                };

                try
                {
                    _positionService.Add(position);

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
                _positionService.Delete(id);
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

            var position = _positionService.Get(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Position, PositionViewModel>()).CreateMapper();
            var positionViewModel = mapper.Map<Position, PositionViewModel>(position);

            return View(positionViewModel);
        }

        [HttpPost]
        public IActionResult Edit(PositionViewModel model, string searchSelectionString, string searchString)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;

            if (ModelState.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PositionViewModel, Position>()).CreateMapper();
                var position = mapper.Map<PositionViewModel, Position>(model);

                try
                {
                    _positionService.Edit(position);

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
