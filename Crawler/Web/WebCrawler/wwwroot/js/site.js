function search() {
    //debugger;
    var pesquisa = $('.search_input').val();
    var validacep = /^[0-9]{5}-[0-9]{3}$/;
    if (validacep.test(pesquisa)) {
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "api/Crawler/Correio",
            contentType:"application/json",
            data: JSON.stringify(pesquisa),
            success: function (result) {

                $('.Rua').text(`Logradouro: ${result["logradouroDNEC"]}.`)
                $('.Estado').text(`Estado: ${result["uf"]}.`)
                $('.Bairro').text(`Bairro: ${result["bairro"]}.`)
                $('.Cidade').text(`Cidade: ${result["localidade"]}.`)
                $('.Cep').text(`Cep: ${result["cep"]}.`)

                $('#produto').hide();
                $('#cotacoes').hide();
                $('#correios').show();
            }
        })
    }
    else if (pesquisa == 'cotações')
    {
        var tabela = $("#cotacoes_table").empty();
        $.ajax({
            type: "GET",
            dataType: "json",
            url: "api/Crawler/Cotacoes",
            success: function (result) {

                var tmplSource = document.getElementById("tmplCotacao").innerHTML;
                var tmplHandle = Handlebars.compile(tmplSource);

                for (var i = 0; i < result.length; i++) {

                    var moeda = {};
                    moeda.name = result[i].name == null ? result[i].exchangeasset : result[i].name; //Nome da moeda
                    moeda.openbidvalue = result[i].openbidvalue == null ? result[i].open : result[i].openbidvalue; //Abriu
                    moeda.askvalue = result[i].askvalue == null ? result[i].price : result[i].askvalue; //Fechou
                    moeda.variationpercentbid = result[i].variationpercentbid == null ? result[i].pctChange : result[i].variationpercentbid //Variação

                    var linha = {};
                    linha.template = document.createElement("template");;
                    linha.template.innerHTML = tmplHandle(moeda)
                    linha.content = document.importNode(linha.template.content, true);
                    tabela[0].appendChild(linha.content);

                };

                $('#produto').hide();
                $('#cotacoes').show();
                $('#correios').hide();
            }
        })
    }
    else
    {
        var tabela = $("#tableGoogle").empty();
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "api/Crawler/BuscaProduto",
            contentType:"application/json",
            data: JSON.stringify(pesquisa),
            success: function (result) {
                //debugger

                var tmplSource = document.getElementById("tmplProduto").innerHTML;
                var tmplHandle = Handlebars.compile(tmplSource);

                var produto = {};
                produto.Valor = result.valor;
                produto.Produto = result.produto;
                produto.Loja = result.loja;

                var linha = {};
                linha.template = document.createElement("template");;
                linha.template.innerHTML = tmplHandle(produto)
                linha.content = document.importNode(linha.template.content, true);
                tabela[0].appendChild(linha.content);

                //for (var i = 0; i < result.length; i++) {

                //    var produto = {};
                //    produto.Valor = result[i].Valor; 
                //    produto.Produto = result[i].Produto; 
                //    produto.Loja = result[i].Loja; 

                //    var linha = {};
                //    linha.template = document.createElement("template");;
                //    linha.template.innerHTML = tmplHandle(produto)
                //    linha.content = document.importNode(linha.template.content, true);
                //    tabela[0].appendChild(linha.content);

                //};

                $('#produto').show();
                $('#cotacoes').hide();
                $('#correios').hide();
            }
        })
    }

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
    $('#produto').hide();
    $('#correios').hide();
    $('#cotacoes').hide();

    $('.search_input').keyup(function (event) {
        if (event.keyCode == 13) {
            console.log('TESTE')
            search();
        }
    });
})
