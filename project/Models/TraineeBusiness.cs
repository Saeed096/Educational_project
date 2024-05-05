using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project.Models.ViewModel;

namespace project.Models
{
    public class TraineeBusiness
    {
         ItiContext context = new ItiContext();
        //ItiContext context; 
        //public TraineeBusiness(ItiContext context)
        //{
        //    this.context = context;

        //}
        public List<Trainee> getAll()
        {
            return context.trainees.ToList();
        }

        public List<string> get_names()
        {
            return context.trainees.Select(t => t.name).ToList();
        }

        public List<int> get_ids()
        {
            return context.trainees.Select(t => t.id).ToList();
        }

        public double get_result(int stu_id , int crs_id)
        {
            return context.crsResults
                .Where(cr=>cr.trainee_id == stu_id && cr.crs_id == crs_id)
                .Select(cr => cr.degree).FirstOrDefault();
        }

         public Trainee get_trainee_by_id(int id)  
        {
             return context.trainees
                .Where(t => t.id == id).Include(t => t.department).Include(t => t.CrsResults)
                .FirstOrDefault();         
        }


        public CourseName_TraineeName_Degree_Color_LIST_VM get_all_results(int stu_id)
        {
            CourseName_TraineeName_Degree_Color_LIST_VM temp =
                new CourseName_TraineeName_Degree_Color_LIST_VM();

            temp.traineeNames.Add(context.trainees.Where(t => t.id == stu_id)
                 .Select(t => t.name).FirstOrDefault());

            List<CrsResult> crsResults = context.crsResults
                .Where(cr => cr.trainee_id == stu_id).ToList();

            List<double> degrees = context.crsResults.Where(crs => crs.trainee_id == stu_id)
                      .Select(crs => crs.degree).ToList();

            if (temp.traineeNames.Count != 0 && crsResults != null)
            {
                int i = 0;
                foreach (CrsResult result in crsResults)
                {
                    temp.course_names.Add(context.courses
                        .Where(c => c.id == result.crs_id)
                        .Select(c => c.name).FirstOrDefault());

                    temp.degrees.Add(degrees[i]);

                    double min_deg = context.courses.Where(c => c.id == crsResults[i].crs_id)
                         .Select(c => c.minDegree).FirstOrDefault();

                    if (result.degree >= min_deg)           
                        temp.colors.Add("green");
                    else
                        temp.colors.Add("red");


                    i++;
                }
            }
            return temp;
            }
            
        }
    }

