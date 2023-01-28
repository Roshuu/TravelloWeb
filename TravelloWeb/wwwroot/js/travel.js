var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {




    function Delete(url) {
        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
                confirmButton: 'btn btn-success',
                cancelButton: 'btn btn-danger'
            },
            buttonsStyling: false
        })

        swalWithBootstrapButtons.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes, delete it!',
            cancelButtonText: 'No, cancel!',
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: url,
                    type: 'DELETE',
                    success: function (data) {
                        if (data.success) {
                            dataTable.ajax.reload();
                            toastr.success(data.message);
                        }
                        else {
                            toastr.error(data.message);
                        }
                    }
                })

            }
        })
    }


    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Travel/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "country", "width": "15%" },
            { "data": "city", "width": "15%" },
            { "data": "price", "width": "15%" },
            { "data": "guide.name", "width": "15%" },
            { "data": "guide.surname", "width": "15%" },

            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role = "group">
                        <a href="/Admin/Travel/Upsert?id=${data}" class="btn btn-primary mx-2" ><i class="bi bi-pencil-square"></i>Edit</a>
                        <a onClick=Delete('/Admin/Travel/Delete/${data}') class="btn btn-danger mx-2"><i class="bi bi-trash"></i>Delete</a>
                        </div >
                    `
                },
                "width": "15%"
            }

        ]
    });


}

