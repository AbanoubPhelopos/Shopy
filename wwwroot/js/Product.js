var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $("#tblData").DataTable({
        "ajax": {
            "url": '/Admin/Product/GetTableData',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": 'name' },
            { "data": 'description' },
            { "data": 'price' },
            { "data": 'category.name' },
            {
                "data": 'id',
                "render": function (data) {
                    return `
                        <div>
                            <a href="/Admin/Product/Edit/${data}" class="btn btn-outline-primary">
                                <i class="bi bi-pencil-square"></i> Edit
                            </a>
                            <a href="/Admin/Product/Delete/${data}" class="btn btn-outline-danger">
                                <i class="bi bi-trash3"></i> Delete
                            </a>
                        </div>
                    `;
                }
            }
        ]
    });
}
