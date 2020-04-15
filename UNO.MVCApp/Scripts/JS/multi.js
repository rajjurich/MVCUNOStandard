﻿/*! multi.js 15-04-2018 */
var multi = function () {
    var a = function (a, b) {
        var c = document.createEvent("HTMLEvents");
        c.initEvent(a, !1, !0), b.dispatchEvent(c)
    },
        b = function (b, c) {
            var d = b.options[c.target.getAttribute("multi-index")];
            d.disabled || (d.selected = !d.selected, a("change", b))
        },
        c = function (a, b) {
            if (a.wrapper.selected.innerHTML = "", a.wrapper.non_selected.innerHTML = "", b.non_selected_header && b.selected_header) {
                var c = document.createElement("div"),
                    d = document.createElement("div");
                c.className = "header", d.className = "header", c.innerText = b.non_selected_header, d.innerText = b.selected_header, a.wrapper.non_selected.appendChild(c), a.wrapper.selected.appendChild(d)
            }
            if (a.wrapper.search) var e = a.wrapper.search.value;
            for (var f = null, g = null, h = 0; h < a.options.length; h++) {
                var i = a.options[h],
                    j = i.value,
                    k = i.textContent || i.innerText,
                    l = document.createElement("a");
                if (l.tabIndex = 0, l.className = "item", l.innerHTML = k, l.setAttribute("role", "button"), l.setAttribute("data-value", j), l.setAttribute("multi-index", h), i.disabled && (l.className += " disabled"), i.selected) {
                    l.className += " selected";
                    var m = l.cloneNode(!0);
                    a.wrapper.selected.appendChild(m)
                }
                if ("OPTGROUP" == i.parentNode.nodeName && i.parentNode != g) {
                    if (g = i.parentNode, f = document.createElement("div"), f.className = "item-group", i.parentNode.label) {
                        var k = document.createElement("span");
                        k.innerHTML = i.parentNode.label, k.className = "group-label", f.appendChild(k)
                    }
                    a.wrapper.non_selected.appendChild(f)
                }
                i.parentNode == a && (f = null, g = null), (!e || e && k.toLowerCase().indexOf(e.toLowerCase()) > -1) && (null != f ? f.appendChild(l) : a.wrapper.non_selected.appendChild(l))
            }
        };
    return function (a, d) {
        if (d = void 0 !== d ? d : {}, d.enable_search = void 0 === d.enable_search || d.enable_search, d.search_placeholder = void 0 !== d.search_placeholder ? d.search_placeholder : "Search...", d.non_selected_header = void 0 !== d.non_selected_header ? d.non_selected_header : null, d.selected_header = void 0 !== d.selected_header ? d.selected_header : null, null == a.dataset.multijs && "SELECT" == a.nodeName && a.multiple) {
            a.style.display = "none", a.setAttribute("data-multijs", !0);
            var e = document.createElement("div");
            if (e.className = "multi-wrapper", d.enable_search) {
                var f = document.createElement("input");
                f.className = "search-input", f.type = "text", f.setAttribute("placeholder", d.search_placeholder), f.addEventListener("input", function () {
                    c(a, d)
                }), e.appendChild(f), e.search = f
            }
            var g = document.createElement("div");
            g.className = "non-selected-wrapper";
            var h = document.createElement("div");
            h.className = "selected-wrapper", e.addEventListener("click", function (c) {
                c.target.getAttribute("multi-index") && b(a, c)
            }), e.addEventListener("keypress", function (c) {
                var d = 32 === c.keyCode || 13 === c.keyCode;
                c.target.getAttribute("multi-index") && d && (c.preventDefault(), b(a, c))
            }), e.appendChild(g), e.appendChild(h), e.non_selected = g, e.selected = h, a.wrapper = e, a.parentNode.insertBefore(e, a.nextSibling), c(a, d), a.addEventListener("change", function () {
                c(a, d)
            })
        }
    }
}();
"undefined" != typeof jQuery && function (a) {
    a.fn.multi = function (b) {
        return b = void 0 !== b ? b : {}, this.each(function () {
            var c = a(this);
            multi(c.get(0), b)
        })
    }
}(jQuery);