var currentList = {}

function createShoppingList() {
    currentList.name = $('#shoppingListName').val();
    currentList.items = new Array();
    //Web service Call

    $('#shoppingListTitle').html(currentList.name);
    $('#shoppingListItems').empty();

    $('#createListDiv').hide();
    $('#shoppingListDiv').show();
}

function addItem() {
    var newItem = {};
    newItem.name = $("#itemName").val();
    currentList.items.push(newItem);
    console.info(currentList);
    drawItems();
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

$(document).ready(function () {
    $('#shoppingListDiv').hide();
})
