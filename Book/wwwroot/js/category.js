// Get the element with id="defaultOpen" and click on it
document.getElementById("defaultOpen").click();
function openCity(evt, Name) {
    // Declare all variables
    var i, tabcontent, tablinks;


    

    // Get all elements with class="tabcontent" and hide them
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }

    // Get all elements with class="tablinks" and remove the class "active"
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }

    // Show the current tab, and add an "active" class to the button that opened the tab
    document.getElementById(Name).style.display = "block";
    evt.currentTarget.className += " active";

}



$(document).ready(function () {

    $('#tableCategory').DataTable({
        "autoWidth": false,
        "responsive": true
    });
    $('#tableLogCategory').DataTable({
        "autoWidth": false,
        "responsive": true
    });
});
function Delete(id) {

    Del(`/Admin/Categories/Delete?id=${id}`)
}

function DeleteLog(id) {

    Del(`/Admin/Categories/DeleteLog?id=${id}`)
}
function Del(path) {
    Swal.fire({
        title: "هل لأنت متأكد؟",
        text: "!لن تتمكن من التراجع عن هذا",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = path
            Swal.fire({
                title: "! تم الحذف ",
                text: "تم حذف الفئة بنجاح",
                icon: "success"
            });
        }
    });
}

Edit = (id, name, description) => {
    $('#title').text(' تعديل مجموعةالفئات');
    $('#btnSave').val(' تعديل ');
    $('#categoryId').val(id);
    $('#categoryName').val(name);
    $('#categorydesc').val(description);
   
   
}
Reset = () => {

    $('#title').text(' أضف مجموعةالفئات');
    $('#btnSave').val(' حفظ');
    $('#categoryId').val('');
    $('#categoryName').val('');
    $('#categorydesc').val('');
   
   
}
