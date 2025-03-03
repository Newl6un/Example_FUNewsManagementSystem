// Update your modal-dialog-manager.js file with this version
const modalDialogManager = {
    loadFormModal: function (url, modalId) {
        $.get(url).done(function (data) {
            $(modalId).find('.modal-body').html(data);
            $(modalId).modal('show');
        });
    },

    submitFormModal: function (form, modalId) {
        $.ajax({
            type: form.attr('method'),
            url: form.attr('action'),
            data: form.serialize(),
            success: function (response) {
                if (response.success) {
                    $(modalId).modal('hide');
                    // Show success toast
                    toastManager.showToast('Operation completed successfully!', 'Success', 'success');
                    // Reload the page after a short delay
                    setTimeout(function () {
                        window.location.reload();
                    }, 1000);
                } else {
                    // If server returns partial view with validation errors
                    $(modalId).find('.modal-body').html(response);
                }
            }
        });
        return false;
    }
};

$(document).ready(function () {
    // Handle modal form submissions
    $(document).on('submit', '.modal-body form', function (e) {
        e.preventDefault();
        modalDialogManager.submitFormModal($(this), '#commonModal');
    });
});

// Separate out delete confirmation logic
const deleteConfirmationManager = {
    // Opens the delete confirmation modal
    showDeleteConfirmation: function (url, itemName, itemType) {
        const modal = $('#deleteConfirmationModal');

        // Set the item name in the confirmation message
        if (itemName) {
            $('#deleteItemName').text(`"${itemName}" (${itemType || 'item'})`);
        } else {
            $('#deleteItemName').text(`this ${itemType || 'item'}`);
        }

        // Set the form's action URL and properly include the anti-forgery token
        const form = $('#deleteConfirmationForm');
        form.attr('action', url);

        // Show the modal
        modal.modal('show');
    },

    // Submit the delete form
    submitDeleteForm: function () {
        const form = $('#deleteConfirmationForm');
        $.ajax({
            type: form.attr('method'),
            url: form.attr('action'),
            data: form.serialize(),
            headers: {
                'X-Requested-With': 'XMLHttpRequest'
            },
            success: function (response) {
                if (response.success) {
                    $('#deleteConfirmationModal').modal('hide');
                    // Show success toast
                    toastManager.showToast('Item deleted successfully!', 'Success', 'success');
                    // Reload the page after a short delay
                    setTimeout(function () {
                        window.location.reload();
                    }, 1000);
                } else {
                    toastManager.showToast('Error deleting item', 'Error', 'danger');
                }
            },
            error: function () {
                toastManager.showToast('Error processing request', 'Error', 'danger');
            }
        });
    }
};

// Create a toast manager
const toastManager = {
    showToast: function (message, title = 'Notification', type = 'info') {
        const toastContainer = document.getElementById('toastContainer');
        if (!toastContainer) {
            console.error('Toast container not found!');
            return;
        }

        const toastId = `toast-${Date.now()}`;
        const toastHtml = `
            <div id="${toastId}" class="toast align-items-center text-white bg-${type} border-0" role="alert" aria-live="assertive" aria-atomic="true">
                <div class="d-flex">
                    <div class="toast-body">
                        <strong>${title}</strong>
                        <div>${message}</div>
                    </div>
                    <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
                </div>
            </div>
        `;

        toastContainer.insertAdjacentHTML('beforeend', toastHtml);

        const toast = new bootstrap.Toast(document.getElementById(toastId), {
            delay: 5000
        });
        toast.show();

        // Remove toast from DOM after it's hidden
        document.getElementById(toastId).addEventListener('hidden.bs.toast', function () {
            this.remove();
        });
    }
};

// Add event handlers when document is ready
$(document).ready(function () {
    // Handler for delete buttons with data attributes
    $(document).on('click', '[data-delete-url]', function (e) {
        e.preventDefault();

        const url = $(this).data('deleteUrl');
        const itemName = $(this).data('deleteName');
        const itemType = $(this).data('deleteType');

        deleteConfirmationManager.showDeleteConfirmation(url, itemName, itemType);
    });

    // Manual initialization for existing delete buttons
    $('.btn-delete').click(function (e) {
        e.preventDefault();

        const url = $(this).attr('href');
        const itemName = $(this).data('name');
        const itemType = $(this).data('type');

        deleteConfirmationManager.showDeleteConfirmation(url, itemName, itemType);
    });

    // Handle delete form submission
    $(document).on('submit', '#deleteConfirmationForm', function (e) {
        e.preventDefault();
        deleteConfirmationManager.submitDeleteForm();
    });
});
