var currentList = {}

function createShoppingList() {
    currentList.name = $('#shoppingListName').val();
    currentList.items = new Array();
    //Web service Call
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "api/Shopping/",
        contentType: "application/json",
        data: JSON.stringify(currentList),
        success: function (result) {
            showShoppingList();
        }
    })
}

function showShoppingList() {
    $('#shoppingListTitle').html(currentList.name);
    $('#shoppingListItems').empty();

    $('#createListDiv').hide();
    $('#shoppingListDiv').show();

    $('#itemName').focus();
    $('#itemName').keyup(function (event) {
        if (event.keyCode == 13) {
            addItem();
        }
    });
}

function addItem() {
    debugger;
    var newItem = {};
    newItem.name = $("#itemName").val();
    newItem.shoppingListId = currentList.id;
    currentList.items.push(newItem);

    $.ajax({
        type: "POST",
        dataType: "json",
        url: "api/Item/",
        contentType: "application/json",
        data: JSON.stringify(newItem),
        success: function (result) {
            currentList = result;
            console.info(currentList);
            $('#itemName').val("");
            drawItems();
        }
    })
}

function drawItems() {
    var $list = $('#shoppingListItems').empty();
    for (var i = 0; i < currentList.items.length; i++) {
        var currentItem = currentList.items[i];
        var $li = $('<li>').html(currentItem.name).attr("id", "item_" + i);
        var $deleteBtn = $("<button onclick='deleteItem(" + i + ")'>D</button>").appendTo($li);
        var $checkBtn = $("<button onclick='checkItem(" + i + ")'>C</button>").appendTo($li);
        $li.appendTo($list);
    };

}

function checkItem(index) {

    if ($("#item_" + index).hasClass("checked")) {
        $("#item_" + index).removeClass("checked");
    }
    else
    {
        $("#item_" + index).addClass("checked");
    }
}

function deleteItem(index) {
    currentList.items.splice(index, 1);
    drawItems();
}

function getShoppingById(id) {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "api/Shopping/" + id,
        success: function (result) {
            currentList = result;
            showShoppingList();
            drawItems();
        },
        error: function () {
            console.error("Bad Request");
        }

    })
}

$(document).ready(function () {
    $('#shoppingListDiv').hide();

    $('#shoppingListName').focus();
    $('#shoppingListName').keyup(function (event) {
        if (event.keyCode == 13) {
            createShoppingList();
        }
    });


    var pageUrl = window.location.href;
    var indexId = pageUrl.indexOf("?id=");
    if (indexId != -1) {
        getShoppingById(pageUrl.substring(indexId + 4));
    }
})
