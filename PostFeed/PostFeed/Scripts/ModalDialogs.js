//http://jqueryui.com/dialog/#modal-form

$(function () {
        //dialog and form for creating new user
    var userDialog, userForm,
        //dialog and form for post creation
        postDialog, postForm,
        name = $("#name"),
        allUserFields = $([]).add(name),
        tips = $(".validateTips");

    function populateAuthorsList(){
        $.ajax({
            type: "GET",
            url: '/api/Authors',
            cache: false,
            success: function (response) {
                var allAuthors = response;
                var authorNames = [];
                var authorIds = [];
                for(var i = 0; i< allAuthors.length; i++){
                    authorNames.push(allAuthors[i].Name);
                    authorIds.push(allAuthors[i].Id);
                }
                var htmlselect = document.createElement('select');
                htmlselect.name = 'Authors';
                htmlselect.id = 'nameSelectList';

                var option;
                for (var i = 0; i < authorNames.length; i++) {
                    option = document.createElement("option");
                    option.value = authorIds[i];
                    option.text = authorNames[i];
                    htmlselect.appendChild(option);
                }
                htmlselect.className += " form-control";
                var selectionElement = document.getElementById("nameSelect");
                while (selectionElement.firstChild){
                    selectionElement.removeChild(selectionElement.firstChild)
                }
                selectionElement.appendChild(htmlselect);
                
            }
        });
    }
    function updateTips(t) {
        tips
            .text(t)
            .addClass("ui-state-highlight");
        setTimeout(function () {
            tips.removeClass("ui-state-highlight", 1000);
        }, 500);
    }

    function checkLength(o, n, min, max) {
        if (o.val().length > max || o.val().length < min) {
            o.addClass("ui-state-error");
            updateTips("Length of " + n + "must be between" +
                min + " and " + max + ".");
            return false;
        } else {
            return true;
        }
    }

    function checkRegexp(o, regexp, n) {
        if (!(regexp.test(o.val()))) {
            o.addClass("ui-state-error");
            updateTips(n);
            return false;
        } else {
            return true;
        }
    }

    function addUser() {
        var valid = true;
        allUserFields.removeClass("ui-state-error");

        valid = valid && checkLength(name, "username", 3, 16);
        valid = valid && checkRegexp(name, /^[a-z]([0-9a-z_\s])+$/i, "Username may consist of a-z, 0-9, underscores, spaces and must begin with a letter.");
        if (valid) {
            //use ajax to add the user
            var data = { Name: name.val(), Active: true };
            $.ajax({
                type: "POST",
                url: '/api/Authors',
                data: JSON.stringify(data),
                contentType: "application/json",
                cache: false,
                success: function (response) {
                    populateAuthorsList();
                }
            });
            userDialog.dialog("close");
        }
        return valid;
    }

    function addPost(){
        postDialog.dialog("close");
    }

    postDialog = $("#new-post-form").dialog({
        autoOpen: false,
        height: 500,
        width: 350,
        modal: true,
        buttons: {
            "Create a new Post": addPost,
            Cancel: function () {
                postDialog.dialog("close");
                }
            },
            close: function () {
                postForm[0].reset();
                allUserFields.removeClass("ui-state-error");
            }
    });

    postForm = postDialog.find("form").on("submit", function (event){
        event.preventDefault();
        addPost();
    });

    $("#newPost").click(function () {
        postDialog.dialog("open");
    });


    userDialog = $("#new-user-form").dialog({
        autoOpen: false,
        height: 200,
        width: 350,
        modal: true,
        buttons: {
            "Create a new User": addUser,
            Cancel: function () {
                userDialog.dialog("close");
            }
        },
        close: function () {
            userForm[0].reset();
            allUserFields.removeClass("ui-state-error");
        }
    });

    userForm = userDialog.find("form").on("submit", function (event) {
        event.preventDefault();
        addUser();
    });

    $("#newUser").click(function () {
        userDialog.dialog("open");
    });
});