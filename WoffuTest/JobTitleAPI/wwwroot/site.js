$(document).ready(function () {

    // Initialize and load the data table
    var datatable = $('#titles_table').DataTable({
        // Ajax source for filling the table
        ajax: {
            url: '/api/jobtitles',
            dataSrc: ''
        },
        // Columns definition
        columns: [
            // Data columns
            { data: 'jobTitleId', title: 'ID' },
            { data: 'name', title: 'Title' },
            { data: 'companyId', title: 'Company' },
            // Buttons column
            {
                data: null,
                width: "15%",
                orderable: false,
                render: function (data, type, row) {
                    return '<button type="button" class="btn btn-sm btn-outline-dark edit-button"><i class="fas fa-edit"></i></button>' +
                           '<button type="button" class="btn btn-sm btn-outline-dark ml-1 delete-button"><i class="fas fa-trash"></i></button>';
                }
            }
        ],
        // Other options and styling
        lengthChange: false
    });

    // Add item button
    $('#add_item').click(function () {
        // Clear the modal
        $('#new_name').val('');
        // Show the modal
        $('#new_item_modal').modal();
    });

    // Click the new item save changes button
    $('#new_save').click(function () {
        // Check form values
        var name_val = $('#new_name').val();
        var company_val = $('#new_company').val();
        if (name_val == '') {
            alert('Please enter a valid name');
        } else if (company_val == '' || isNaN(company_val)) {
            alert('The company ID must be a number');
        } else {
            // Submit the change
            $.ajax({
                url: '/api/jobtitles',
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({
                    name: name_val,
                    companyId: parseInt(company_val)
                }),
                error: function (j, status) {
                    alert('A server error has occurred');
                },
                complete: function (data) {
                    // Hide the modal
                    $('#new_item_modal').modal('hide');
                    // Reload the table
                    datatable.ajax.reload();
                }
            });
        }
    });

    // Item being edited
    var edited_item;
    // Click the edit item button
    $('#titles_table tbody').on('click', '.edit-button', function () {
        // Get the data for this row and save it
        edited_item = datatable.row($(this).parents('tr')).data();
        // Fill the modal with the current name
        $('#edit_name').val(edited_item.name);
        // Show the modal
        $('#edit_item_modal').modal();
    });

    // Click the edit save changes button
    $('#edit_save').click(function () {
        if ($('#edit_name').val() == '') {
            alert('Please enter a valid name');
        } else {
            // Change the item's name
            edited_item.name = $('#edit_name').val();
            // Submit the change
            $.ajax({
                url: '/api/jobtitles/' + edited_item.jobTitleId,
                type: 'PUT',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(edited_item),
                error: function (j, status) {
                    alert('A server error has occurred');
                },
                complete: function (data) {
                    // Hide the modal
                    $('#edit_item_modal').modal('hide');
                    // Reload the table
                    datatable.ajax.reload();
                }
            });
        }
    });

    // Click the delete item button
    $('#titles_table tbody').on('click', '.delete-button', function () {
        // Get the data for this row
        var deleted_item = datatable.row($(this).parents('tr')).data();
        // Confirmation
        if (!confirm('Really delete this item? This action cannot be undone')) return;
        // Submit the change
        $.ajax({
            url: '/api/jobtitles/' + deleted_item.jobTitleId,
            type: 'DELETE',
            error: function (j, status) {
                alert('A server error has occurred');
            },
            complete: function (data) {
                // Reload the table
                datatable.ajax.reload();
            }
        }); 
    });

});