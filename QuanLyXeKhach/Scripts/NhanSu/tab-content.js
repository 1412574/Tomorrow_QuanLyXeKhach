function myFunction(x) {
    $("#product_view").modal();
};




$('.star').on('click', function () {
    $(this).toggleClass('star-checked');
});

$('.ckbox label').on('click', function () {
    $(this).parents('tr').toggleClass('selected');
});

function filter(x) {
    var $target = $(x).data('target');
    if ($(x).hasClass("show")) {
        //        $('.table-inside tr').css('display', 'none');
        $('.table-inside tr[data-status="' + $target + '"]').fadeOut('slow');
        $(x).removeClass("show");
    }
    else {
        $('.table-inside tr[data-status="' + $target + '"]').css('display', 'none').fadeIn('slow');
        $(x).addClass("show");
    }
};

function openPage(pageName, elmnt) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablink");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].style.backgroundColor = "#fff";
    }
    document.getElementById(pageName).style.display = "block";
    elmnt.style.backgroundColor = "#eee";
    elmnt.style.color = black;

};


$(document).on("click", ".open-RemoveDialog", function () {
    var myId = $(this).data('id');
    $(".modal-body #Id").val(myId);

    // As pointed out in comments, 
    // it is superfluous to have to manually call the modal.
    //$('#item_edit').modal('show');
});

$(document).on("click", ".open-AddBookDialog", function () {
    var myBookId = $(this).data('id');
    $(".modal-body #bookId").val(myBookId);
});

$('#my_modal').on('show.bs.modal', function (e) {
    var bookId = $(e.relatedTarget).data('book-id');
    $(e.currentTarget).find('input[name="bookId"]').val(bookId);
});


function ConfirmDelete(currentId) {
    $.ajax({
        type: "Get",
        url: '@Url.Action("ConfirmDelete", "UngVien")',
        data: { id: currentId },
        success: function (data) {
            $('#PVConfirmDelete').html(data);
            $('#item_remove').modal('show');
        }
    })
}
function Delete(currentId) {
    $.ajax({
        type: "Get",
        url: '@Url.Action("Delete", "UngVien")',
        data: { id: currentId },
        success: function (data) {
            //notification here
        }
    })
}

function NVPaging(x) {
    var $target = $(x).data('target');
    if ($(x).hasClass("btn-primary")) {
        //$('.table-inside tr[data-status="' + $target + '"]').fadeOut('slow');
        //$(x).removeClass("btn-primary");
        //$(x).removeClass("active");
    }
    else {
        var active = document.getElementsByClassName('NVPaging btn btn-xs filter sharp btn-primary');
        $('.table-inside .NVtr').hide();
        $('.pagination .NVPaging').removeClass("btn-primary");

        $('.table-inside tr[data-status="' + $target + '"]').css('display', 'none').show();
        $(x).addClass("btn-primary");
    }
};

function CVPaging(x) {
    var $target = $(x).data('target');
    if ($(x).hasClass("btn-primary")) {
        //$('.table-inside tr[data-status="' + $target + '"]').fadeOut('slow');
        //$(x).removeClass("btn-primary");
        //$(x).removeClass("active");
    }
    else {
        var active = document.getElementsByClassName('CVPaging btn btn-xs filter sharp btn-primary');
        $('.table-inside .CVtr').hide();
        $('.pagination .CVPaging').removeClass("btn-primary");

        $('.table-inside tr[data-status="' + $target + '"]').css('display', 'none').show();
        $(x).addClass("btn-primary");
    }
};

function ChonNV(maNV) {
    $('#maNV').val(maNV);
};

function ChonCV(maCV) {
    $('#maCV').val(maCV);
};
