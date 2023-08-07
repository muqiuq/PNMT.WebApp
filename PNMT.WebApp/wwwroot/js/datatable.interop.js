
var dataTable;

export function attachDataTable(target) {
    $(document).ready(function () {
        dataTable = $(target).DataTable();
    });
 
}

export function detachDataTable() {
    dataTable.destroy();
}
