var currentList = {}

function createShoppingList() {
    currentList.name = $('#shoppingListName').val();
    currentList.items = new Array();
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "api/ShoppingEf/",
        contentType: "application/json",
        data: JSON.stringify(currentList),
        success: function (result) {
            currentList = result;
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
    var newItem = {};
    newItem.name = $("#itemName").val();
    newItem.shoppingListId = currentList.id;
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "api/ItemsEf/",
        contentType: "application/json",
        data: JSON.stringify(newItem),
        success: function (result) {
            debugger;
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
        var $deleteBtn = $("<button onclick='deleteItem(" + currentItem.id + ")'>D</button>").appendTo($li);
        var $checkBtn = $("<button onclick='checkItem(" + currentItem.id + ")'>C</button>").appendTo($li);
        if (currentItem.checked) {$li.addClass("checked")}
        $li.appendTo($list);
    };
}

function checkItem(itemId) {
    var changeItem;

    for (var i = 0; i < currentList.items.length; i++) {
        if (currentList.items[i].id == itemId) {
            changeItem = currentList.items[i];
        }
    };

    changeItem.checked = !changeItem.checked;
    $.ajax({
        type: "PUT",
        dataType: "json",
        url: "api/ItemsEf/",
        contentType: "application/json",
        data: JSON.stringify(changeItem),
        success: function () {
            getShoppingById(currentList.id);
        }
    });
}

function deleteItem(itemId) {
    var item;
    for (var i = 0; i < currentList.items.length; i++) {
        if (currentList.items[i].id == itemId) {
            item = currentList.items[i];
        }
    }

    $.ajax({
        type: "DELETE",
        dataType: "json",
        url: "api/ItemsEf/",
        data: JSON.stringify(item),
        success: function (result) {
            currentList = result;
            drawItems();
        }
    });
    drawItems();
}

function getShoppingById(id) {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "api/ShoppingEf/" + id,
        success: function (result) {
            currentList = result;
            showShoppingList();
            drawItems();
        },
        error: function (result) {
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
