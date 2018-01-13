;(function($) {

if(typeof String.prototype.trim !== 'function') {
  String.prototype.trim = function() {
    return this.replace(/^\s+|\s+$/g, ''); 
  }
}

var appVersion = navigator.appVersion;
window.ieVersion = null;
var MSIEIndex = appVersion.indexOf("MSIE");
if(MSIEIndex!=-1)
{
	ieVersion = parseInt(appVersion.substring(MSIEIndex + "MSIE".length,appVersion.indexOf(".",MSIEIndex)).trim());
}

window.isSafari = /constructor/i.test(window.HTMLElement);

var window_width, window_height,

overflow_y_applied = false;

column_list_details = {
	'ul.region_list': {
		column_condition: {
			'5': function() {
				return window_width >= 780;
			},
			'3': function() {
				return window_width >= 420 && window_width < 780;
			}
		},
		last_row_center: true
	},
	'.country_list_wrapper ul': {
		column_condition: {
			'7': function() {
				return window_width >= 900;
			},
			'6': function() {
				return window_width >= 780 && window_width < 900;
			},
			'5': function() {
				return window_width >= 600 && window_width < 780;
			},
			'4': function() {
				return window_width >= 480 && window_width < 600;
			},
			'3': function() {
				return window_width >= 420 && window_width < 480;
			}
		},
		last_row_center: false
	}
};

$(document).ready(function(e) {
    calculate_window_dimension();

    $('footer').html("&copy; " + (new Date()).getFullYear() + " VFS Global Group. All Rights Reserved.");
	
	set_column_list_classes();
	
	if( ieVersion == null || ieVersion > 8 )
		set_image_onload_attr($('ul.region_list > li > span > a > .image_hover_wrapper > .normal > img'), 'map_image_loaded(this);');
	
	var region_dropdown = $('.region_dropdown_wrapper select'),
	country_dropdown = $('.country_dropdown_wrapper select');
	
	$('ul.region_list > li > span > a').each(function(index, element) {
		var $this = $(this),
        option = $('<option>');
		
		option.attr('value', $this.attr('data-filter'));
		option.html($this.children('span').html());
		
		region_dropdown.append(option);
    });
	
	region_dropdown.change(function(e) {
        var val = $(this).val();
		
		country_dropdown.html('');
		country_dropdown.append("<option value='#'>Select Country</option>");
		
		if( val != '#' )
		{
			if( val == '*' )
				fill_country_dropdown(true);
			else
				fill_country_dropdown(function($this) {
					return $this.is(val);
				});
		}
		
		set_msDropdown(country_dropdown);
    });
	
	region_dropdown.trigger('change');
	
	set_msDropdown(region_dropdown);
	
	country_dropdown.change(function(e) {
		var $this = $(this),
        val = $this.val(),
		target = $this.children('option:selected').attr('data-target');
		
		if( val != '#' )
		{
			if( target == undefined || target == '_self' )
			{
				window.location = val;
			}
			else
			{
				window.open(val);
			}
		}
    });
});

$(window).load(function (e) {
    var country_list_wrapper = $('.country_list_wrapper'),
    country_list_ul = country_list_wrapper.find('ul'),
    all_country_li = country_list_ul.children('li'),
	all_region_link = $('ul.region_list > li > span > a'),
	region_click_scroll_duration = 500,
	isotope_obj = new custom_isotope(country_list_ul, {
		tween_duration: 500,
		transition_duration: 500,
		visible_children_by_display: 'table'
	}),
	save_extra_isotope_styles_obj = {
		country_list_wrapper: {
			styles: {
				'height': {
					set_value: function(el) {
						return parseInt(el.css('height'));
					},
					get_css: custom_isotope.set_styles_methods.append_px_after_value
				}
			}
		},
		el_childrens: {
			styles: {
				'height': {
					set_value: function(el) {
						return parseInt(el.css('height'));
					},
					get_css: custom_isotope.set_styles_methods.append_px_after_value
				}
			}
		}
	},
	isotope_animation_type = 'transition';
	
	if( ieVersion != null && ieVersion <= 9 )
		isotope_animation_type = 'tween';
	
	if( ieVersion != null && ieVersion <= 8 )
	{
		isotope_obj.options.hidden_el_animation_details = [
			{
				style_prop_name: 'opacity',
				value_ranges: {
					'fade': {
						start_val: 0,
						end_val: 1
					}
				},
				prop_val: function(values) {
					return values['fade'];
				}
			}
		];
	}
	
	isotope_obj.country_list_wrapper = country_list_wrapper;
	
	all_region_link.click(function(e, is_start) {
		var $this = $(this),
		selected_filter = $this.attr('data-filter');
		
		if( is_start )
		{
			all_country_li.filter(':not(' + selected_filter + ')').addClass('hidden');
			set_all_country_list_methods();
		}
		else if( !$this.hasClass('active') && !isotope_obj.animation_running )
		{
			var active_region_link = all_region_link.filter('.active'),
			scroll_top = $(window).scrollTop(),
			html_body = $('html, body'),
			body_height,
			animate_scroll_top;
			
			isotope_obj.save_isotope_styles('current', save_extra_isotope_styles_obj);
			
			active_region_link.removeClass('active');
			$this.addClass('active');
			
			filter_country_li(selected_filter);
			
			isotope_obj.save_isotope_styles('new', save_extra_isotope_styles_obj);
			
			animate_scroll_top = country_list_wrapper.offset().top;
			
			body_height = $('body').outerHeight();
			
			if( animate_scroll_top + window_height > body_height )
				animate_scroll_top = body_height - window_height;
			
			if( animate_scroll_top < 0 )
				animate_scroll_top = 0;
			
			filter_country_li(active_region_link.attr('data-filter'));
			
			html_body.stop().scrollTop(scroll_top);
			html_body.animate({scrollTop: animate_scroll_top}, region_click_scroll_duration);
			
			setTimeout(function() {
				isotope_obj.animate_isotope_styles(isotope_animation_type, 'current', 'new');
				
				isotope_obj.on_animation_complete_callback = function() {
					filter_country_li(selected_filter);
				};
			}, 10);
		}
		
		return false;
	});
	
	all_region_link.filter('.active').trigger('click', true);

	country_list_wrapper.mCustomScrollbar();
	
	if (ieVersion <= 8 && ieVersion != null)
		set_IE8_background_size($('body'), 'cover', 'top', 'center', 'fixed');
	
	$(window).resize(function(e) {
		overflow_y_applied = false;
		
		remove_isotope_styles();
        calculate_window_dimension();
		set_all_country_list_methods();
		resize_msDropdown();
		
		if (ieVersion <= 8 && ieVersion != null)
			set_IE8_background_size($('body'), 'cover', 'top', 'center', 'fixed');
    });
});

function filter_country_li(selected_filter)
{
	var country_list_wrapper = $('.country_list_wrapper'),
	all_country_li = country_list_wrapper.find('ul > li');
	
	all_country_li.removeClass('hidden');
	
	if( selected_filter != '*' )
		all_country_li.filter(':not(' + selected_filter + ')').addClass('hidden');
	
	set_all_country_list_methods(country_list_wrapper, true);
}

function set_all_country_list_methods(content_wrapper, reset_classes)
{
	set_column_list_classes(content_wrapper, reset_classes);
	set_equal_children_height(content_wrapper);
	set_column_list_center(content_wrapper);
	set_country_list_wrapper_height();
}

function remove_isotope_styles()
{
	$('.country_list_wrapper ul > li').each(function(index, element) {
        var $this = $(this);
		
		this.isotope_styles = {
			'position': $this.css('position'),
			'left': $this.css('left'),
			'top': $this.css('top')
		};
		
		$this.css({
			'position': '',
			'left': '',
			'top': ''
		});
    });
}

function fill_country_dropdown(condition)
{
	var all_country_list_li = $('.country_list_wrapper ul > li'),
	country_dropdown = $('.country_dropdown_wrapper select');
	
	all_country_list_li.each(function(index, element) {
        var $this = $(this);
		
		if( condition == true || condition($this) )
		{
			var cur_link = $this.children('span').children('a'),
			option = $('<option>');
			
			option.attr({
				'value': cur_link.attr('href'),
				'data-target': cur_link.attr('target')
			});
			
			option.html(cur_link.children('span').text());
			
			country_dropdown.append(option);
		}
    });
}

function set_msDropdown(select_el)
{
	var data_dd = select_el.data('dd');
	if( data_dd != undefined )
		data_dd.destroy();
	
	select_el.msDropdown();
	select_el.closest('.ddOutOfVision').next('.ddcommon').children('.ddChild').mCustomScrollbar();
}

function set_image_onload_attr($el, onload_script)
{
    $el.each(function (index, element) {
        var $this = $(this),
        this_src = $this.attr('src');

        $this.attr('src', '');

        $this.attr('onload', onload_script);

        $this.attr('src', this_src);
    });
}

function set_country_list_wrapper_height()
{
    var country_list_wrapper = $('.country_list_wrapper'),
    country_list_wrapper_min_height = 250,
    country_list_wrapper_height = parseFloat(country_list_wrapper.find('ul').css('height')),
    country_list_wrapper_max_height = window_height - country_list_wrapper.offset().top - $(window).scrollTop() - $('footer').outerHeight() - 20 - parseFloat(country_list_wrapper.css('padding-top')) - parseFloat(country_list_wrapper.css('padding-bottom'));
	
	if( !overflow_y_applied )
		$('body').css('overflow-y', '');
	
    if (country_list_wrapper_height > country_list_wrapper_max_height && country_list_wrapper_height > country_list_wrapper_min_height)
    {
        if (country_list_wrapper_max_height < country_list_wrapper_min_height)
		{
            country_list_wrapper_height = country_list_wrapper_min_height;
			$('body').css('overflow-y', 'scroll');
			overflow_y_applied = true;
		}
        else
		{
            country_list_wrapper_height = country_list_wrapper_max_height;
		}
    }
	
    country_list_wrapper.css('height', country_list_wrapper_height + 'px');
}

function set_column_list_classes(content_wrapper, reset_classes)
{
	if( content_wrapper == undefined )
		content_wrapper = $('body');
	
	for( var key in column_list_details )
	{
		var column_list = content_wrapper.find(key);
		
		if( column_list.length != 0 )
		{
			var cur_column_list_details = column_list_details[key],
			cur_column_condition_details = cur_column_list_details.column_condition,
			all_li = column_list.children('li'),
			non_hidden_li = all_li.filter(':not(.hidden)'),
			cur_column = column_list.attr('data-column');
			
			for( var column in cur_column_condition_details )
			{
				var column_condition = cur_column_condition_details[column];
				
				if( column_condition() )
				{
					if( cur_column != column || reset_classes == undefined || reset_classes == true )
					{
						column = parseInt(column);
						
						column_list.attr('data-column', column);
						
						if( cur_column != undefined )
						{
							column_list.removeClass("col" + cur_column);
						}
							
						column_list.addClass("col" + column);
						
						all_li.removeClass('first_in_row last_in_row first_row last_row');
						
						var last_row_index = Math.floor((non_hidden_li.length - 1) / column);
						
						non_hidden_li.each(function(index, element) {
							var $this = $(this);
							
							if( index % column == 0 )
								$this.addClass('first_in_row');
							
							if( index % column == column - 1 )
								$this.addClass('last_in_row');
							
							if( index < column )
								$this.addClass('first_row');
							
							if( Math.floor(index / column) == last_row_index )
								$this.addClass('last_row');
						});
					}
					
					break;
				}
			}
		}
	}
}

function set_column_list_center(content_wrapper)
{
	if( content_wrapper == undefined )
		content_wrapper = $('body');
	
	for( var key in column_list_details )
	{
		var column_list = content_wrapper.find(key);
		
		if( column_list.length != 0 )
		{
			var cur_column_list_details = column_list_details[key],
			all_li = column_list.children('li'),
			non_hidden_li = all_li.filter(':not(.hidden)'),
			cur_column_condition_details = cur_column_list_details.column_condition,
			last_row_first_li,
			all_last_row_li,
			last_row_last_li,
			last_row_first_li_margin_left;
			
			for( var column in cur_column_condition_details )
			{
				var column_condition = cur_column_condition_details[column];
				
				if( column_condition() )
				{
					all_li.css('margin-left', '');
					
					if( cur_column_list_details.last_row_center && non_hidden_li.filter('.first_row').length != non_hidden_li.filter('.last_row').length )
					{
						last_row_first_li = non_hidden_li.filter('.last_row.first_in_row');
						all_last_row_li = non_hidden_li.filter('.last_row');
						last_row_last_li = $(all_last_row_li[all_last_row_li.length - 1]);
						
						last_row_first_li_margin_left = ( column_list.offset().left + column_list.outerWidth() - last_row_last_li.offset().left - last_row_last_li.outerWidth() ) / 2;
						
						last_row_first_li.css('margin-left', last_row_first_li_margin_left + 'px');
					}
					
					break;
				}
			}
		}
	}
}

if( ieVersion == null || ieVersion > 8 )
{
	window.hover_canvas = $('<canvas>');
	window.hover_canvas_ctx = hover_canvas[0].getContext("2d");
	window.map_image_loaded = function(img)
	{
		var $img = $(img),
		region_list = $img.closest('ul.region_list'),
		hover_wrapper = $img.closest('.normal').next('.hover');
		
		if( hover_wrapper.children('img').length == 0 )
		{
			region_list.css('display', 'block');
			
			$img.css({
				'width': 'auto',
				'height': 'auto'
			});
			
			var image_dimension = {
				width: $img.width(),
				height: $img.height()
			};
			
			hover_canvas.attr({
				'width': image_dimension.width,
				'height': image_dimension.height
			});
			
			$img.css({
				'width': '',
				'height': ''
			});
			
			hover_canvas_ctx.drawImage(img, 0, 0, image_dimension.width, image_dimension.height);
			
			var image_data = hover_canvas_ctx.getImageData(0, 0, image_dimension.width, image_dimension.height),
			image_color_data = image_data.data,
			r, g, b, a;
			
			for( var i = 0 ; i < image_color_data.length ; i += 4 )
			{
				r = image_color_data[i];
				g = image_color_data[i + 1];
				b = image_color_data[i + 2];
				a = image_color_data[i + 3];
				
				if( a != 0 && b - r > 10 && b - g > 10 )
				{
					image_color_data[i] = 232;
					image_color_data[i + 1] = 96;
					image_color_data[i + 2] = 32;
				}
			}
			
			hover_canvas_ctx.putImageData(image_data, 0, 0);
			
			hover_wrapper.append("<img src='" + hover_canvas[0].toDataURL("image/jpeg", 1) + "' alt='" + $img.attr('alt') + "' />");
			
			region_list.css('display', '');
		}
	}
}

function set_equal_children_height(content_wrapper)
{
	if( content_wrapper == undefined )
		content_wrapper = $('body');
	
	content_wrapper.find('[data-equal_children_height]').each(function(index, element) {
        var $this = $(this),
		children_selector = $this.attr('data-equal_children_height'),
		childrens = $this.children(':not(.hidden)'),
		same_offset_top_children = [],
		previous_children_offset_top = null,
		max_height = [],
		cur_array_index = null;
		
		childrens.css('height', '');
		
		childrens.each(function(index, element) {
			var cur_children = $(this),
			cur_main_children_el = children_selector == '.' ? cur_children : cur_children.find(children_selector),
			cur_main_children_el_height = cur_main_children_el.outerHeight(),
			cur_children_offset_top = cur_children.offset().top;
			
			if( previous_children_offset_top != null && previous_children_offset_top != cur_children_offset_top )
			{
				same_offset_top_children[cur_array_index].css('height', (max_height[cur_array_index] + 1) + 'px');
				cur_children_offset_top = cur_children.offset().top;
			}
			
			if( previous_children_offset_top != null && previous_children_offset_top == cur_children_offset_top )
			{
				same_offset_top_children[cur_array_index] = same_offset_top_children[cur_array_index].add(cur_main_children_el);
			}
			else
			{
				same_offset_top_children.push(cur_main_children_el);
				max_height.push(0);
				cur_array_index = same_offset_top_children.length - 1;
			}
			
			if( max_height[cur_array_index] < cur_main_children_el_height )
				max_height[cur_array_index] = cur_main_children_el_height;
			
			previous_children_offset_top = cur_children_offset_top;
		});
		
		same_offset_top_children[cur_array_index].css('height', (max_height[cur_array_index] + 1) + 'px');
    });
}

function set_IE8_background_size(el, back_size, position_x, position_y, back_attachment) {
	el.css("background-image", "");
	el.find(".background_image").remove();
	var el_width, el_height;
	if (el.prop("tagName").toLowerCase() == "body") {
		el_width = window_width;
		el_height = window_height;
		if (el_width < el.width()) {
			el_width = el.width();
		}
		if (el_height < el.height()) {
			el_height = el.height();
		}
	}
	else {
		el_width = el.width();
		el_height = el.height();
	}
	var el_ratio = el_width / el_height;
	
	var back_src = get_src_from_back_img(el);
	el.css("background-image", "none");
	el.append("<div class='background_image'><img src='" + back_src + "' /></div>");
	var el_back = el.find(".background_image");
	el_back.css("width", el_width + "px");
	el_back.css("height", el_height + "px");

	if (back_attachment == "fixed") {
		el_back.css('position', 'fixed');
	}

	var el_back_img = el.find(".background_image img");
	var el_back_img_width = el_back_img.width();
	var el_back_img_height = el_back_img.height();
	var el_back_img_ratio = el_back_img_width / el_back_img_height;

	var el_back_img_new_width, el_back_img_new_height, el_back_img_new_left, el_back_img_new_top;
	if (back_size == "cover") {
		if (el_ratio < el_back_img_ratio) {
			el_back_img_new_width = Math.round(el_height * el_back_img_width / el_back_img_height);
			el_back_img_new_height = Math.round(el_height);
		}
		else {
			el_back_img_new_width = Math.round(el_width);
			el_back_img_new_height = Math.round(el_width * el_back_img_height / el_back_img_width);
		}
	}
	else if (back_size == "contain") {
		if (el_ratio < el_back_img_ratio) {
			el_back_img_new_width = Math.round(el_width);
			el_back_img_new_height = Math.round(el_width * el_back_img_height / el_back_img_width);
		}
		else {
			el_back_img_new_width = Math.round(el_height * el_back_img_width / el_back_img_height);
			el_back_img_new_height = Math.round(el_height);
		}
	}
	else {
		var el_back_img_new_dimension = back_size.split(' ');
		el_back_img_new_width = Math.round(parseFloat(el_back_img_new_dimension[0]));
		el_back_img_new_height = Math.round(parseFloat(el_back_img_new_dimension[1]));
	}

	if (position_x == "center") {
		el_back_img_new_left = el_width / 2 - el_back_img_new_width / 2;
	}
	else {
		el_back_img_new_left = parseInt(position_x);
	}

	if (position_y == "center") {
		el_back_img_new_top = el_height / 2 - el_back_img_new_height / 2;
	}
	else {
		el_back_img_new_top = parseInt(position_y);
	}

	el_back_img.css("width", el_back_img_new_width + "px");
	el_back_img.css("height", el_back_img_new_height + "px");
	el_back_img.css("left", el_back_img_new_left + "px");
	el_back_img.css("top", el_back_img_new_top + "px");
}

function get_src_from_back_img(el) {
	var src = text_substring(el.css("background-image"), "url(", ")");
	if (src != null) {
		if ((src.indexOf("'") == 0 && src.lastIndexOf("'") == (src.length - 1)) ||
			(src.indexOf("\"") == 0 && src.lastIndexOf("\"") == (src.length - 1))) {
			src = src.substring(1, src.length - 1);
		}
	}
	return src;
}

function text_substring(str, start_str, end_str) {
	var result = null;
	var start_index = str.indexOf(start_str);
	var end_index = str.indexOf(end_str, str.indexOf(start_str) + start_str.length);
	if (start_index != -1 && end_index != -1) {
		result = str.substring(start_index + start_str.length, end_index);
	}
	return result;
}

function resize_msDropdown()
{
	$(".dd").each(function(index, element) {
		$(this).css("width", $(this).prev(".ddOutOfVision").find("select").css("width"));
    });
}

function calculate_window_dimension()
{
	if( isSafari )
	{
		window_width = $("html").width();
		window_height = $("html").height();
	}
	else
	{
		window_width = window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth;
		window_height = window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight;
	}
}

})(jQuery);