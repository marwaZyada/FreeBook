

$(document).ready(function () {
    $('#tableRole').DataTable({
        "autoWidth": false,
        "responsive": true
    });
});
function Delete(id) {
 
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
            window.location.href =`/Admin/Accounts/DeleteRole?id=${id}`
            Swal.fire({
                title: "! تم الحذف ",
                text: "تم حذف المجموعة بنجاح",
                icon: "success"
            });
        }
    });
}

Edit = (id, name) => {
     $('#roleId').val(id);
    $('#roleName').val(name);
    $('#title').text(' تعديل مجموعةالمستخدم');
    $('#btnSave').val('تعديل ');
   
   
}
Reset = () => {
    $('#roleId').val('');
    $('#roleName').val('');
    $('#title').text(' أضف مجموعةالمستخدم');
    $('#btnSave').val(' حفظ');
}