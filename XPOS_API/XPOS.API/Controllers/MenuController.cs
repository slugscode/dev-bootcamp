using Microsoft.AspNetCore.Mvc;
using XPOS.API.Models;
using XPOS_ViewModels;

namespace XPOS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly XPos_315Context db;
        
        public MenuController(XPos_315Context _db)
        {
            this.db = _db;
        }

        [HttpGet("MenuParent/{IdRole}")]
        public List<VMMenu> MenuParent(int IdRole)
        {
            List<VMMenu> dataMenu = new List<VMMenu>();

            dataMenu = (from men in db.TblMenus
                        join ma in db.TblMenuAccesses
                        on men.Id equals ma.IdMenu
                        where men.IsParent == true && ma.IdRole == IdRole
                        select new VMMenu
                        {
                            
                            MenuName = men.MenuName,
                            MenuAction = men.MenuAction,

                            MenuController = men.MenuController,
                            MenuIcon = men.MenuIcon,
                            MenuSorting = men.MenuSorting,

                            MenuParent = men.MenuParent,
                            IdMenu = ma.IdMenu,
                            IdRole = ma.IdRole,
                         

                            listMenuChild = (from menChild in db.TblMenus
                                             join maChild in db.TblMenuAccesses

                                             on menChild.Id equals maChild.IdMenu

                                             where menChild.IsDelete == false && menChild.IsParent ==false && menChild.MenuParent == men.Id && maChild.IdRole == IdRole

                                             select new VMMenu
                                             {
                                                 
                                                 MenuName = menChild.MenuName,
                                                 MenuAction = menChild.MenuAction,

                                                 MenuController = menChild.MenuController,
                                                 MenuIcon = menChild.MenuIcon,
                                                 MenuSorting = menChild.MenuSorting,
                                                 MenuParent = menChild.MenuParent,

                                                 IdMenu = maChild.IdMenu,
                                                 IdRole = maChild.IdRole



                                             }).ToList()
                        }

                ).ToList();

            return dataMenu;
        }
    }
}
