var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $("#tblData1").DataTable({
        "ajax": {
            "url": '/Admin/Category/GetTableData',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": 'name' },
            { "data": 'description' },
            { "data": 'createdTime' },
            {
                "data": 'id',
                "render": function (data) {
                    return `
                        <div>
                            <a href="/Admin/Category/Edit/${data}" class="btn btn-outline-primary">
                                <i class="bi bi-pencil-square"></i> Edit
                            </a>
                            <a href="/Admin/Category/Delete/${data}" class="btn btn-outline-danger">
                                <i class="bi bi-trash3"></i> Delete
                            </a>
                        </div>
                    `;
                }
            }
        ]
    });
}