using project.Models.ViewModel;

namespace project.Models
{
    public class CrsResultBusiness
    {
        ItiContext context = new ItiContext();
        //ItiContext context;
        //public CrsResultBusiness(ItiContext context)
        //{
        //    this.context = context;
        //}
        public CourseName_TraineeName_Degree_Color_LIST_VM get_crs_results_details(int crs_id)
        {
             CourseName_TraineeName_Degree_Color_LIST_VM temp = new CourseName_TraineeName_Degree_Color_LIST_VM();    // if v.m has only obj and i make list here >> field like course name will has many values >> size of list >> although all have same value >> i wanna store it once but if list is inside v.m >> ican control ???????? ok

            string crs_name = context.courses.Where(c => c.id == crs_id)
                .Select(c =>c.name).FirstOrDefault();

            double min_deg = context.courses.Where(c => c.id == crs_id)
                    .Select(c => c.minDegree).FirstOrDefault();

            List<CrsResult> course_results = context.crsResults
                .Where(cr => cr.crs_id == crs_id).ToList();
                        

            if (crs_name != null && course_results != null) 
            {
                temp.course_names.Add(crs_name);

                //List<string> trainee_names = context.trainees.Where(t => t.id == crsResult.trainee_id)
                //    .Select(t => t.name).ToList();

                foreach(CrsResult crs in course_results) 
                {
/*!!*/                 temp.traineeNames.Add(context.trainees.Where(t => t.id == crs.trainee_id)
                      .Select(t => t.name).FirstOrDefault());

                    temp.degrees.Add(crs.degree);


                    if (crs.degree >= min_deg)            
                        temp.colors.Add("green");
                    else
                        temp.colors.Add("red");
                }

            }
            return temp;

        }
    }
}
