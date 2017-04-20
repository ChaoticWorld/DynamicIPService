$(function () {
    var trs = $("table").rows;
    if (trs) {
        for (i = 1; i < trs.length; i++) {
            if (i % 2 == 0)
                tr.className += "alt";
        } 
    }
})
