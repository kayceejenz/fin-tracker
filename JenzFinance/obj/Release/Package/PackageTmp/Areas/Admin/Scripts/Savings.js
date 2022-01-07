
function EditCategory(Id) {
    $.ajax({
        url: "/Savings/GetCategory/" + Id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#IdEdit').val(result.Id);
            $('#DescriptionEdit').val(result.Description);
        },
        error: function (errormessage) {
            var message = errormessage.responseText;
            toastr.error(message, "Data not retrieved successfully", { showDuration: 500 })
        }
    });
}
function DeleteCategory(ID) {
    Swal.fire({
        title: 'Confirmation',
        text: "Are you sure, you want to delete this?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "/Savings/DeleteCategory/" + ID,
                type: "Post",
                contentType: "application/json;charset=UTF-8",
                dataType: "json",
                success: function () {
                    Swal.fire({
                        position: 'top-end',
                        icon: 'success',
                        title: 'Deleted successfully',
                        showConfirmButton: false,
                        timer: 1500
                    })
                    window.location.reload(true);
                },
                error: function () {
                    Swal.fire({
                        position: 'top-end',
                        icon: 'error',
                        title: 'Delete Failed',
                        showConfirmButton: false,
                        timer: 1500
                    })
                }
            })
        }
        else if (
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire(
                'Cancelled',
                'Deactivation cancelled :)',
                'error'
            )
        }
    })
}

function EditSubCategory(Id) {
    $.ajax({
        url: "/Savings/GetSubCategory/" + Id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#IdSEdit').val(result.Id);
            $('#DescriptionSEdit').val(result.Description);
            $('#AmountPerMonthSEdit').val(result.AmountPerMonth);
            $('#NumberOfMonthsSEdit').val(result.NumberOfMonths);
            $('#ParentIDSEdit').val(result.ParentID);
        },
        error: function (errormessage) {
            var message = errormessage.responseText;
            toastr.error(message, "Data not retrieved successfully", { showDuration: 500 })
        }
    });
}
function DeleteSubCategory(ID) {
    Swal.fire({
        title: 'Confirmation',
        text: "Are you sure, you want to delete this?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "/Savings/DeleteSubCategory/" + ID,
                type: "Post",
                contentType: "application/json;charset=UTF-8",
                dataType: "json",
                success: function () {
                    Swal.fire({
                        position: 'top-end',
                        icon: 'success',
                        title: 'Deleted successfully',
                        showConfirmButton: false,
                        timer: 1500
                    })
                    window.location.reload(true);
                },
                error: function () {
                    Swal.fire({
                        position: 'top-end',
                        icon: 'error',
                        title: 'Delete Failed',
                        showConfirmButton: false,
                        timer: 1500
                    })
                }
            })
        }
        else if (
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire(
                'Cancelled',
                'Deactivation cancelled :)',
                'error'
            )
        }
    })
}
const swalWithBootstrapButtons = Swal.mixin({
    customClass: {
        confirmButton: 'btn btn-success',
        cancelButton: 'btn btn-danger'
    },
    buttonsStyling: false
})
function Activate(ID) {
    Swal.fire({
        title: 'Confirmation',
        text: "Are you sure, you want to activate this?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, activate it!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "/Savings/ActivateSaving/" + ID,
                type: "Post",
                contentType: "application/json;charset=UTF-8",
                dataType: "json",
                success: function () {
                    Swal.fire({
                        position: 'top-end',
                        icon: 'success',
                        title: 'Activated successfully',
                        showConfirmButton: false,
                        timer: 1500
                    })
                    window.location.reload(true);
                },
                error: function () {
                    Swal.fire({
                        position: 'top-end',
                        icon: 'error',
                        title: 'Activation Failed',
                        showConfirmButton: false,
                        timer: 1500
                    })
                }
            })
        }
        else if (
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire(
                'Cancelled',
                'Actication cancelled :)',
                'error'
            )
        }
    })
}

$("#update").click(function () {
    let arrObj = [];
    let tr = $("#target > tr");
    $.each(tr, function (i, tablerow) {
        var td = tablerow.children[3].children[0];
        if (!(td === undefined)) {
            let checked = td.checked;
            if (checked) {
                let datePaid = tablerow.children[4].children[0].value;
                let id = tablerow.children[0].dataset.id;

                let obj = {
                    Id: id,
                    DatePaid: datePaid,
                    HasPaid: checked
                };

                arrObj.push(obj);
            }
        }
    });
    if (arrObj.length > 0) {
        $.ajax({
            url: "UpdateSavingRecord",
            type: "Post",
            data: JSON.stringify(arrObj),
            dataType: "Json",
            contentType: "application/json;charset:utf-8",
            success: function (response) {
                if (response > 0) {
                    alert("Record Updated successfully.");
                    window.location.reload(true);
                }
            },
            error: function (e) {
                alert(e.responseText);
            }
        });
    } else {
        alert("No data to update!");
    }
})
