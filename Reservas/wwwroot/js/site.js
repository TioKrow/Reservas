// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

(function ($) {
    'use strict';

    var $accountDelete = $('#delete-dialogo'),
        $accountDeleteDialog = $('#confirm-dialogo');

    $accountDelete.on('click', function () {
        $accountDeleteDialog[0].showModal();
    });

    $('#Cancelar').on('click', function () {
        $accountDeleteDialog[0].close();
    });
    $('#Guardar').on('click', function () {
        $accountDeleteDialog[0].close();
    });


})(jQuery);
(function ($) {
    'use strict';

    var $accountDelete = $('#delete2-dialogo'),
        $accountDeleteDialog = $('#confirm-dialogo');

    $accountDelete.on('click', function () {
        $accountDeleteDialog[0].showModal();
    });

    $('#Cancelar').on('click', function () {
        $accountDeleteDialog[0].close();
    });
    $('#Guardar').on('click', function () {
        $accountDeleteDialog[0].close();
    });


})(jQuery);