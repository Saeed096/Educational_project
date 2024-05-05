function update_courses(crsId)
{
    //console.log(crsId);
    var id = $("#dept_id").val();
/**/ // cannot detect last selected course >> when reload page due to invalid data?????
    $.ajax({
/**/        url: `/course/get_courses_by_dept_id/${id}`,  // no work if without '?'  ??????
        success: function (result)
        {
            console.log(result); 
            $("#crs_id").empty();
            if (crsId != undefined) {
                for (let item of result) {

                    if (item.id == crsId)
                        $("#crs_id").append("<option selected>" + item.name + "</option>");

                    else
                        $("#crs_id").append("<option>" + item.name + "</option>");
                }
            }
            else        // can ignore it?????? s.s
            {
                for (let item of result) {
                        $("#crs_id").append("<option>" + item.name + "</option>");
                }
            }
        }
    });
}
