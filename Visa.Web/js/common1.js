if (typeof String.prototype.trim !== 'function') {
    String.prototype.trim = function () {
        return this.replace(/^\s+|\s+$/g, '')
    }
}
function toTitleCase(str) {
    return str.replace(/\w\S*/g, function (txt) {
        return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase()
    })
}
function replace_break(str, replace_str) {
    return str.replace("<br>", replace_str).replace("<BR>", replace_str).replace("<br >", replace_str).replace("<br />", replace_str)
}
$(document).ready(function (e) {
    $("header .fsizer").css("display", "none");
    $("header ul.language_list").css("display", "none");
    $("header .header2").addClass("padt20");
    $("header .header2").removeClass("header2");
    $("header .fsizer #fs_lrg").addClass("fs_disabled");
    var middle_titles = null;
    $('.submenu').each(function (index, element) {
        var submenu_el = $(this);
        submenu_el.find('a').each(function (index, element) {
            var submenu_a_el = $(this);
            submenu_a_el.html(toTitleCase(submenu_a_el.html().replace('&amp;', '&').replace('&nbsp;', ' ')))
        })
    });
    $('.tourist_content').hide();
    $('.nav_visatypes li a').click(function (e, from_object) {
        $('.nav_visatypes li a').removeClass('nav_active');
        var get_name = $(this).attr('name');
        $('.tourist_content').hide();
        $('#' + get_name).show();
        $(this).addClass('nav_active');
        if (from_object == undefined || from_object == null) {
            $(".nav_visatypes_select select option[value]").removeAttr("selected");
            $(".nav_visatypes_select select option[value=\"" + get_name + "\"]").attr("selected", "selected");
            $(".nav_visatypes_select select").trigger('change', this)
        }
        return false
    });
    $('.nav_visatypes li:first-child a').trigger('click');
    $('.r_step_content').html($('#stepOne').html());
    $('.responsive_nav_parent').html('');
    if ($("nav").length != 0) {
        $("<select />").appendTo(".responsive_nav_parent").addClass('responsive_nav');
        $("<option />", {
            "selected": "selected",
            "value": "#",
            "text": "Menu"
        }).appendTo(".responsive_nav_parent select");
        $("nav a").each(function () {
            var el = $(this);
            var menu_text = el.text();
            var menu_target = el.attr("target");
            var parent_submenu = el.closest("div.submenu");
            var prepend_text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            if (((parent_submenu.find("a.visa-category-Sub-head").html() != null || parent_submenu.find('div.semibold').html() != null || parent_submenu.find('div.submenu_head').html() != null) && (!(el.hasClass("visa-category-Sub-head") || el.parent("div.semibold").html() != null || el.parent("div.submenu_head").html() != null))) || (parent_submenu.find("table th").html() != null && el.parent("th").html() == null)) {
                prepend_text = prepend_text + prepend_text + "--&nbsp;"
            }
            if (parent_submenu.html() != null) {
                menu_text = prepend_text + menu_text
            }
            $(".responsive_nav_parent select").append("<option value=\"" + el.attr("href") + "\"" + ((menu_target == undefined || menu_target == null) ? ("") : (" target=\"" + menu_target + "\"")) + " >" + menu_text + "</option>")
        });
        $(".responsive_nav_parent select option:eq(1)").html(' 首页');
        $('.responsive_nav').change(function () {
            if ($(this).val() != '#') {
                if ($(this).find("option:selected").attr("target") == "_blank") window.open($(this).val());
                else window.location.href = $(this).val()
            }
        })
    }
    $('.nav_visatypes_select').html('');
    $("<select />").appendTo(".nav_visatypes_select");
    $(".nav_visatypes a").each(function () {
        var el = $(this);
        $("<option />", {
            "value": el.attr("name"),
            "text": el.text()
        }).appendTo(".nav_visatypes_select select")
    });
    $(document).on('change', '.nav_visatypes_select select', function (e, from_object) {
        if (from_object == undefined || from_object == null) {
            var get_name = $(this).attr('value');
            $(".nav_visatypes_select select option[value]").removeAttr("selected");
            $(".nav_visatypes_select select option[value=\"" + get_name + "\"]").attr("selected", "selected");
            $(".nav_visatypes a[name=\"" + get_name + "\"]").trigger("click", this)
        }
        return false
    });
    if ($(".nav_visatypes").length != 0) {
        var docUrl = document.URL;
        if (docUrl.indexOf("#") != -1) {
            var tab_name = docUrl.substring(docUrl.lastIndexOf("#") + 1);
            $(".nav_visatypes a[name=\"" + tab_name + "\"]").trigger("click")
        }
    }
    if ($(".nav_visatypes").length != 0) {
        $("a.change_tab").click(function (e) {
            var el = $(this);
            var el_href = el.attr("href");
            if (el_href != undefined) {
                if (el_href.indexOf("#") != -1) {
                    var tab_name = el_href.substring(el_href.lastIndexOf("#") + 1);
                    $(".nav_visatypes a[name=\"" + tab_name + "\"]").trigger("click")
                }
            }
        })
    }
    $('.scrollbar1, .scrollbar2').tinyscrollbar({
        sizethumb: 64
    });
    $("input, textarea, select, button").uniform();
    $('nav ul > li').hover(function () {
        $(this).find('.submenu').show()
    }, function () {
        $(this).find('.submenu').hide()
    });
    $("#mycarousel").jcarousel({
        scroll: 1,
        initCallback: mycarousel_initCallback
    });
    $(window).scroll(function () {
        if ($(window).scrollTop() == 0) {
            $('.up_top').fadeOut()
        } else {
            $('.up_top').fadeIn()
        }
    });
    $('.up_top a').click(function () {
        $('html, body').animate({
            scrollTop: 0
        }, 500)
    });
    $('.form_list').change(function () {
        if ($(this).val() != '#') {
            $("#visaform1").attr("href", $(this).val());
            document.getElementById('visaform1').click();
        }
    });
    var randomNumber = Math.floor((Math.random() * 3) + 1);
    if (randomNumber == 1) {
        $('.banner').css('background', 'url(/images/banners/home-page-1.jpg) top center no-repeat')
    } else if (randomNumber == 2) {
        $('.banner').css('background', 'url(/images/banners/home-page-2.jpg) top center no-repeat')
    } else if (randomNumber == 3) {
        $('.banner').css('background', 'url(/images/banners/home-page-3.jpg) top center no-repeat')
    }
    $('.pad_home').not('.home_active').hover(function () {
        $(this).find('img').attr('src', '/images/home_white.png')
    }, function () {
        $(this).find('img').attr('src', '/images/home.png')
    });
    $('.pad_home.home_active').find('img').attr('src', '/images/home_white.png');
    $(".home_step_responsive .ico a").removeClass('ico_active');
    $(".steps ul.step_icons").find('li.ico').each(function (index, element) {
        var el = $(this);
        if (el.find('a').hasClass('ico_active')) {
            $(".home_step_responsive .ico.step0" + (index + 1) + " a").addClass('ico_active')
        }
    });
    if ($(".quickLinks").length != 0) {
        var quicklinks_heading = "Quick Links";
        var total_quicklinks_ul = 5;
        var all_max_links_in_ul = new Array();
        var new_quicklinks = new Array();
        var new_quicklinks_length = 0;
        $("nav a").each(function (index, element) {
            var nav_a = $(this);
            var is_in_submenu = nav_a.closest(".submenu").length != 0;
            var is_in_general_submenu = false;
            var is_in_array = $(new_quicklinks).filter(function (index) {
                return RegExp("^" + replace_break(nav_a.html().replace("&nbsp;", " "), " ").trim().replace(/\s+/g, ' ').replace("(", "\\(").replace(")", "\\)") + "$", "i").test(replace_break($(this).html().replace("&nbsp;", " "), " ").trim().replace(/\s+/g, ' '))
            }).length != 0;
            if (is_in_submenu) {
                is_in_general_submenu = $(nav_a.closest(".submenu").closest("li").find("a")[0]).attr("href").match(/^general_information.html$/i) != null
            }
            var add_link = (!is_in_submenu || is_in_general_submenu) && nav_a.attr("href").match(/^index.html$/i) == null && nav_a.attr("href").match(/^general_information.html$/i) == null && nav_a.attr("href").match(/^useful_links.html$/i) == null && !is_in_array;
            if (add_link) {
                new_quicklinks[new_quicklinks_length] = nav_a;
                new_quicklinks_length++
            }
        });
        var all_quicklinks_ul = new Array();
        var links_in_quicklinks_ul = new Array();
        $(".quickLinks").html("<h5 class=\"c_blue marb10\">" + quicklinks_heading + "</h5>");
        var temp_links_added = 0;
        for (var i = 0; i < total_quicklinks_ul; i++) {
            $(".quickLinks").append("<ul id=\"cur_quicklinks_ul\" class=\"floatl" + (i < total_quicklinks_ul - 1 ? " marr20" : "") + "\"></ul>");
            var cur_quicklinks_ul = $(".quickLinks").find("ul#cur_quicklinks_ul");
            all_quicklinks_ul.push(cur_quicklinks_ul);
            links_in_quicklinks_ul.push(0);
            cur_quicklinks_ul.removeAttr("id");
            all_max_links_in_ul[i] = (all_max_links_in_ul[i] == null) ? (Math.ceil((new_quicklinks_length - temp_links_added) / (total_quicklinks_ul - i))) : (all_max_links_in_ul[i]);
            temp_links_added += all_max_links_in_ul[i]
        }
        var ul_position = 0;
        var link_text = null;
        var checking_in_ul = 0;
        for (var i = 0; i < new_quicklinks_length; i++) {
            if (links_in_quicklinks_ul[ul_position] < all_max_links_in_ul[ul_position]) {
                link_text = "<li><a" + (new_quicklinks[i].attr("href") != null ? " href=\"" + new_quicklinks[i].attr("href") + "\"" : "") + (new_quicklinks[i].attr("title") != null ? " title=\"" + new_quicklinks[i].attr("title") + "\"" : "") + (new_quicklinks[i].attr("target") != null ? " target=\"" + new_quicklinks[i].attr("target") + "\"" : "") + ">" + new_quicklinks[i].attr("title") + "</a></li>";
                all_quicklinks_ul[ul_position].append(link_text);
                links_in_quicklinks_ul[ul_position]++;
                ul_position = (((ul_position + 1) == total_quicklinks_ul) ? 0 : (ul_position + 1));
                checking_in_ul = 0
            } else {
                if (checking_in_ul < total_quicklinks_ul - 1) {
                    ul_position = (((ul_position + 1) == total_quicklinks_ul) ? 0 : (ul_position + 1));
                    i--;
                    checking_in_ul++
                } else {
                    break
                }
            }
        }
    }
    $("body *").each(function (index, element) {
        var el = $(this);
        var el_tag = el.prop("tagName").toUpperCase();
        if (!el.hasClass("dont_convert")) {
            if (el_tag == "STRONG" || el_tag == "B") {
                el.replaceWith("<span class=\"semibold\">" + el.html() + "</span>")
            } else if (el.css("font-weight") == "bold" || el.css("font-weight") == "bolder" || parseFloat(el.css("font-weight")) >= 600) {
                el.contents().filter(function (index) {
                    return this.nodeType === 3
                }).each(function (index, element) {
                    $(this).wrap("<span class=\"semibold\"></span>")
                })
            }
        }
    })
});

function mycarousel_initCallback(carousel) {
    $('.jcarousel-control a:eq(0)').addClass('thumb_active');
    $('.jcarousel-control a').bind('click', function () {
        $('.jcarousel-control a').removeClass('thumb_active');
        $(this).addClass('thumb_active');
        carousel.scroll(jQuery.jcarousel.intval(jQuery(this).attr('id')));
        return false
    })
};
$(window).load(function (e) {
    if ($("#cycle_banner_wrapper1 .cycle_banner").length != 0) {
        $("#cycle_banner_wrapper1 .cycle_banner").cycle({
            fx: 'scrollLeft',
            timeout: 5000,
            speed: 700,
            pager: '#cycle_banner_wrapper1 .cycle_pager',
            pagerAnchorBuilder: function (index, DOMelement) {
                var pagerAnchor = $('<a>');
                pagerAnchor.attr('href', '#');
                if (index == this.elements.length - 1) pagerAnchor.addClass('last');
                return pagerAnchor
            }
        })
    }
    if ($("#cycle_banner_wrapper2 .cycle_banner").length != 0) {
        $("#cycle_banner_wrapper2 .cycle_banner").cycle({
            fx: 'scrollLeft',
            timeout: 5000,
            speed: 700,
            pager: '#cycle_banner_wrapper2 .cycle_pager',
            pagerAnchorBuilder: function (index, DOMelement) {
                var pagerAnchor = $('<a>');
                pagerAnchor.attr('href', '#');
                if (index == this.elements.length - 1) pagerAnchor.addClass('last');
                return pagerAnchor
            }
        })
    }
});