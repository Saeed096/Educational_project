function send_deleted_item(name , id)
{
    document.getElementById("text").innerText = `${name} course will be deleted permanently..\n Are you sure to delete it?`;
    document.getElementById("delete_btn").href = `/course/delete?deleted_id=${id}`; 
}