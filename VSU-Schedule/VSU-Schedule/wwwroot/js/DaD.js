$(document).ready(function () {
    $(".droppable").droppable({
        drop: function (event, ui) {
            var $list = $(this);
            $helper = ui.helper;

            // Check if we reached the maximum number of children. 
            if ($(this).children().length == $(this).data('max')) {
                return false;
            }

            $($helper).removeClass("selected");
            var $selected = $(".selected");
            if ($selected.length > 1) {
                moveSelected($list, $selected);
            } else {
                moveItem(ui.draggable, $list);
            }
        }, tolerance: "touch"
    });

    $(".draggable").draggable({
        revert: "invalid",
        helper: "clone",
        cursor: "move",
        drag: function (event, ui) {
            var $helper = ui.helper;
            $($helper).removeClass("selected");
            var $selected = $(".selected");
            if ($selected.length > 1) {
                $($helper).html($selected.length + " items");
            }
        }
    });

    function moveSelected($list, $selected) {
        $($selected).each(function () {
            $(this).fadeOut(function () {
                $(this).appendTo($list).removeClass("selected").fadeIn();
            });
        });
    }

    function moveItem($item, $list) {
        $item.fadeOut(function () {
            $item.find(".item").remove();
            $item.appendTo($list).fadeIn();
        });
    }

    $(".item").click(function () {
        $(this).toggleClass("selected");
    });

});