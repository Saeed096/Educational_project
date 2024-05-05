using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using project.Models;
using project.Models.ViewModel;

namespace project.Controllers
{
    public class InstructorController : Controller
    {
        //ItiContext context;
        //public InstructorController(ItiContext context)
        //{
        //    this.context = context;
        //}
        public InstructorController(IWebHostEnvironment iWebHostEnvironment)
        {
            this.iWebHostEnvironment = iWebHostEnvironment;
        }

        InstructorBusiness InstructorBusiness = new InstructorBusiness();
        DepartmentBusiness DepartmentBusiness = new DepartmentBusiness();
        CourseBusiness CourseBusiness = new CourseBusiness();
        ItiContext context = new ItiContext();
        private readonly IWebHostEnvironment iWebHostEnvironment;

        public IActionResult index()
        {
            List<Instructor> instructors = InstructorBusiness.getAll();
            return View( "index" , instructors); 
        }

        public IActionResult detail(int id)
        {
            Instructor instructor = InstructorBusiness.getById(id);
            return View("detail", instructor);
        }

        [HttpGet]
        public IActionResult add()  
        {
            Ins_with_dep_crs_lists_VM model = new Ins_with_dep_crs_lists_VM();
           
            model.crs_names_ids = CourseBusiness
                .get_all_names_ids_by_dept_id(context.departments.Select(d=>d.id).FirstOrDefault());

            model.deps_names_ids = DepartmentBusiness.get_all_names_ids();
            return View("add" , model);
        }

        [HttpPost]
        public IActionResult save_add(Instructor ins ,[FromForm] IFormFile img)    
        { 
            if(ins.name != null && ins.salary != 0)
            {
            
                if(img != null)  
                {
					string wwwRootPath = iWebHostEnvironment.WebRootPath;
					string imagesPath = Path.Combine(wwwRootPath, "img");
                    string imgName = Guid.NewGuid().ToString() + "_" + img.FileName;
					string insImgPath = Path.Combine(imagesPath , imgName);

                    using (FileStream fileStream = new FileStream(insImgPath, FileMode.Create)) // create actual file in this path  // file stream consume many resourses put it in using to limit its scope and consuming {} >> f.s used to write and read on h.w
                    {
                        img.CopyTo(fileStream);   // copy img to this file 
                    }
                    ins.image = imgName; 
				}
                // here u can make else and put default pic 

				context.Add(ins);
                context.SaveChanges(); 
                return RedirectToAction("index");
            }
            Ins_with_dep_crs_lists_VM model = new Ins_with_dep_crs_lists_VM();
            model.name = ins.name;
            model.address = ins.address;
            model.id = ins.id;
            model.image = ins.image;
            model.dept_id = ins.dept_id;
            model.crs_id = ins.crs_id;
            model.salary = ins.salary;
            model.crs_names_ids = CourseBusiness.get_all_names_ids_by_dept_id(ins.dept_id);
            model.deps_names_ids = DepartmentBusiness.get_all_names_ids();

            return View("add" , model);
        }

        public IActionResult update(int id)
        {
            Instructor ins = InstructorBusiness.getById(id);
            Ins_with_dep_crs_lists_VM model = new Ins_with_dep_crs_lists_VM();
            model.id = ins.id;
            model.name = ins.name;
            model.address = ins.address;
            model.dept_id = ins.dept_id;
            model.image = ins.image;
            model.salary = ins.salary;
            model.crs_id = ins.crs_id;
            model.deps_names_ids = DepartmentBusiness.get_all_names_ids().ToList();
            model.crs_names_ids = CourseBusiness.get_all_names_ids().ToList();
            return View("update" , model);
        }

        public IActionResult save_update(Ins_with_dep_crs_lists_VM model)
        {
            if(model.name != null && model.salary > 0 
                && model.dept_id != 0 && model.crs_id != 0)
            {
                Instructor ins = new Instructor();
                ins.id = model.id;
                ins.name = model.name;
                ins.address = model.address;
                ins.salary = model.salary;
                ins.image = model.image;
                ins.dept_id= model.dept_id;
                ins.crs_id= model.crs_id;
                context.Update(ins);
                context.SaveChanges();
                return RedirectToAction("index");
            }
            model.crs_names_ids = CourseBusiness.get_all_names_ids();
            model.deps_names_ids = DepartmentBusiness.get_all_names_ids(); 
            return View("update", model);
        }

        //public IActionResult test (IFormFile img)
        //{

        //}
    }
}
