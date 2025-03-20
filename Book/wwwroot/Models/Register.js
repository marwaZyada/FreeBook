

$(document).ready(function () {
 
    $('#tableUser').DataTable({
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
            window.location.href = `/Admin/Accounts/DeleteUser?id=${id}`
            Swal.fire({
                title: "! تم الحذف ",
                text: "تم حذف المجموعة بنجاح",
                icon: "success"
            });
        }
    });
}

Edit = (id, name, image, email, isactive, role) => {
    $('#imagehide').val(image)
    $('#UserId').val(id);
    $('#UserName').val(name);
    $('#title').text(' تعديل مجموعةالمستخدم');
    $('#btnSave').val('تعديل ');
    $('#UserEmail').val(email);
    $('#Image').show();
    $('#Image').attr('src',`/Images/Users/${image}` );
    $('#ddlUserRole').val(role);
    if (isactive == 'True')
        $('#UserActive').prop('checked', true)
    else
        $('#UserActive').prop('checked', false)
    
    $('#grPassword').hide();
    $('#grCompare').hide();
    $('#UserPassword').val('$$$$$')
    $('#UserCompare').val('$$$$$')
}
Reset = () => {
  
    $('#title').text(' أضف مجموعةالمستخدم');
    $('#btnSave').val(' حفظ');
    $('#UserId').val('');
    $('#UserName').val('');
    $('#imagehide').val('')
    $('#UserEmail').val('');
    $('#Image').hide();
    $('#Image').attr('src', '');
    $('#ddlUserRole').val('');
    $('#UserActive').prop('checked', false)
    $('#grPassword').show();
    $('#grCompare').show()
    $('#UserPassword').val('')
    $('#UserCompare').val('')
}
ChangePassword = (id) => {
    $('#UserpassId').val(id)
}