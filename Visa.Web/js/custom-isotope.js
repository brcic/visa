;(function($) {

'use strict';

var default_options = {
	tween_duration: 700,
	tween_easing: Power2.easeOut,
	transition_duration: 700,
	transition_easing: 'ease',
	visible_children_by_display: 'block',
	hidden_el_animation_details: [
		{
			style_prop_name: 'transform',
			value_ranges: {
				'scale': {
					start_val: 0,
					end_val: 1
				}
			},
			prop_val: function(values) {
				return 'scale(' + values['scale'] + ', ' + values['scale'] + ')';
			}
		}
	],
	auto_delete_all_memory_on_animation_complete: true
},

set_styles_methods = {
	append_px_after_value: function(key, values) {
		return values[key] + 'px';
	}
},

istotope_element_css_values_obj = {
	el: {
		styles: {
			'height': {
				set_value: function(el) {
					return el.height();
				},
				get_css: set_styles_methods.append_px_after_value
			}
		}
	},
	el_childrens: {
		on_start_set_css: {
			'position': 'absolute'
		},
		styles: {
			'left': {
				set_value: function(el) {
					return el.offset().left - el.parent().offset().left;
				},
				get_css: set_styles_methods.append_px_after_value
			},
			'top': {
				set_value: function(el) {
					return el.offset().top - el.parent().offset().top;
				},
				get_css: set_styles_methods.append_px_after_value
			}
		}
	}
},

transition_end_event_str = 'webkitTransitionEnd otransitionend oTransitionEnd msTransitionEnd transitionend';

window.custom_isotope = function(el, options) {
	this.el = el;
	this.options = $.extend(true, {}, default_options, options);
	
	this.el_childrens = el.children();
	
	this.isotope_styles_memory = {};
	
	this.animation_running = false;
	
	this.transition_extra_css = (this.options.transition_duration / 1000) + 's ' + this.options.transition_easing;
	
	var hidden_el_animation_details = this.options.hidden_el_animation_details,
	hidden_el_transition_css = '';
	
	for( var i = 0 ; i < hidden_el_animation_details.length ; i++ )
	{
		if( i != 0 )
			hidden_el_transition_css += ', ';
		
		hidden_el_transition_css += hidden_el_animation_details[i].style_prop_name + ' ' + this.transition_extra_css;
	}
	
	this.hidden_el_transition_css = hidden_el_transition_css;
	
	var el_position = el.css('position').toLowerCase();
	
	if( el_position != 'relative' && el_position != 'absolute' && el_position != 'fixed' )
		el.css('position', 'relative');
};

custom_isotope.prototype.remove_all_isotope_styles = function(from_memory_id, to_memory_id) {
	var _this = this,
	hidden_el_animation_details = _this.options.hidden_el_animation_details,
	from_isotope_styles = _this.isotope_styles_memory[from_memory_id],
	to_isotope_styles = _this.isotope_styles_memory[to_memory_id],
	cur_from_isotope_styles_values,
	cur_to_isotope_styles_values,
	cur_from_on_start_set_css;
	
	for( var key in from_isotope_styles )
	{
		cur_from_on_start_set_css = from_isotope_styles[key].on_start_set_css;
		
		if( cur_from_on_start_set_css != undefined )
		{
			for( var style_key in cur_from_on_start_set_css )
				_this[key].css(style_key, '');
		}
		
		_this[key].each(function(index, element) {
			var $this = $(this),
			cur_from_isotope_styles_values = from_isotope_styles[key].values[index],
			cur_to_isotope_styles_values = to_isotope_styles[key].values[index];
			
			if( cur_from_isotope_styles_values != 'hidden' && cur_to_isotope_styles_values != 'hidden' )
			{
				for( var style_key in cur_from_isotope_styles_values )
					$this.css(style_key, '');
			}
			else if( cur_from_isotope_styles_values == 'hidden' && cur_to_isotope_styles_values != 'hidden' )
			{
				$this.css('display', '');
				
				for( var style_key in cur_to_isotope_styles_values )
					$this.css(style_key, '');
				
				for( var i = 0 ; i < hidden_el_animation_details.length ; i++ )
					$this.css(hidden_el_animation_details[i].style_prop_name, '');
			}
			else if( cur_from_isotope_styles_values != 'hidden' && cur_to_isotope_styles_values == 'hidden' )
			{
				$this.css('display', '');
				
				for( var style_key in cur_from_isotope_styles_values )
					$this.css(style_key, '');
				
				for( var i = 0 ; i < hidden_el_animation_details.length ; i++ )
					$this.css(hidden_el_animation_details[i].style_prop_name, '');
			}
		});
	}
};

custom_isotope.prototype.save_isotope_styles = function(memory_id, extra_isotope_styles) {
	var _this = this,
	el_offset = _this.el.offset(),
	el_height = _this.el.height(),
	cur_isotope_isotope_styles_memory = $.extend(true, {}, istotope_element_css_values_obj),
	css_values,
	values_obj;
	
	if( extra_isotope_styles != undefined )
		$.extend(true, cur_isotope_isotope_styles_memory, extra_isotope_styles);
	
	_this.isotope_styles_memory[memory_id] = cur_isotope_isotope_styles_memory;
	
	for( var key in cur_isotope_isotope_styles_memory )
	{
		css_values = [];
		
		_this[key].each(function(index, element) {
			var $this = $(this);
			
			if( $this.css('display') == 'none' )
			{
				css_values.push('hidden');
			}
			else
			{
				values_obj = {};
				
				for( var style_key in cur_isotope_isotope_styles_memory[key].styles )
					values_obj[style_key] = cur_isotope_isotope_styles_memory[key].styles[style_key].set_value($this);
				
				css_values.push(values_obj);
			}
		});
		
		cur_isotope_isotope_styles_memory[key].values = css_values;
	}
};

custom_isotope.prototype.delete_isotope_styles = function(memory_id) {
	this.isotope_styles_memory[memory_id] = undefined;
};

custom_isotope.prototype.animate_isotope_styles = function(animation_type, from_memory_id, to_memory_id) {
	var _this = this,
	animation_state = {
		value: 0
	},
	from_isotope_styles = _this.isotope_styles_memory[from_memory_id],
	to_isotope_styles = _this.isotope_styles_memory[to_memory_id];
	
	_this.animation_running = true;
	
	_this.all_animation_values = {};
	
	_this.all_total_isotope_styles_applied = {};
	
	for( var key in from_isotope_styles )
	{
		if( from_isotope_styles[key].on_start_set_css != undefined )
			_this[key].css(from_isotope_styles[key].on_start_set_css);
		
		_this[key].each(function(index, element) {
			var cur_from_isotope_styles_values = from_isotope_styles[key].values[index],
			cur_to_isotope_styles_values = to_isotope_styles[key].values[index];
			
			if( 
				( cur_from_isotope_styles_values == 'hidden' && cur_to_isotope_styles_values != 'hidden' ) ||
				( cur_from_isotope_styles_values != 'hidden' && cur_to_isotope_styles_values == 'hidden' )
			)
			{
				$(this).style('display', _this.options.visible_children_by_display, 'important');
			}
		});
	}
	
	_this.set_isotope_styles(_this, from_memory_id, to_memory_id, animation_state);
	
	if( animation_type == 'tween' )
	{
		_this.set_tween(from_memory_id, to_memory_id, animation_state);
	}
	else
	{
		setTimeout(function() {
			_this.set_transition(from_memory_id, to_memory_id, animation_state);
		}, 10);
	}
};

custom_isotope.prototype.set_isotope_styles = function(_this, from_memory_id, to_memory_id, animation_state) {
	var from_isotope_styles = _this.isotope_styles_memory[from_memory_id],
	to_isotope_styles = _this.isotope_styles_memory[to_memory_id],
	cur_from_isotope_styles_values,
	cur_to_isotope_styles_values;
	
	for( var key in from_isotope_styles )
	{
		_this.all_total_isotope_styles_applied[key] = 0;
		
		_this[key].each(function(index, element) {
			var $this = $(this),
			cur_from_isotope_styles_values = from_isotope_styles[key].values[index],
			cur_to_isotope_styles_values = to_isotope_styles[key].values[index];
			
			if( JSON.stringify(cur_from_isotope_styles_values) != JSON.stringify(cur_to_isotope_styles_values) )
				_this.all_total_isotope_styles_applied[key]++;
			
			if( cur_from_isotope_styles_values != 'hidden' && cur_to_isotope_styles_values != 'hidden' )
			{
				for( var style_key in cur_from_isotope_styles_values )
					_this.all_animation_values[style_key] = _this.get_value_on_animation_state(cur_from_isotope_styles_values[style_key], cur_to_isotope_styles_values[style_key], animation_state);
				
				for( var style_key in cur_from_isotope_styles_values )
					$this.css(style_key, from_isotope_styles[key].styles[style_key].get_css(style_key, _this.all_animation_values));
			}
			else if( cur_from_isotope_styles_values == 'hidden' && cur_to_isotope_styles_values != 'hidden' )
			{
				for( var style_key in cur_to_isotope_styles_values )
					_this.all_animation_values[style_key] = cur_to_isotope_styles_values[style_key];
				
				for( var style_key in cur_to_isotope_styles_values )
					$this.css(style_key, to_isotope_styles[key].styles[style_key].get_css(style_key, _this.all_animation_values));
				
				_this.set_hidden_el_styles($this, animation_state);
			}
			else if( cur_from_isotope_styles_values != 'hidden' && cur_to_isotope_styles_values == 'hidden' )
			{
				for( var style_key in cur_from_isotope_styles_values )
					_this.all_animation_values[style_key] = cur_from_isotope_styles_values[style_key];
				
				for( var style_key in cur_from_isotope_styles_values )
					$this.css(style_key, from_isotope_styles[key].styles[style_key].get_css(style_key, _this.all_animation_values));
				
				_this.set_hidden_el_styles($this, animation_state, true);
			}
		});
	}
};

custom_isotope.prototype.get_value_on_animation_state = function(start_val, end_val, animation_state) {
	return ( end_val - start_val ) * animation_state.value + start_val;
};

custom_isotope.prototype.set_hidden_el_styles = function(el, animation_state, do_inverse) {
	var hidden_el_animation_details = this.options.hidden_el_animation_details,
	start_val,
	end_val,
	cur_value_ranges;
	
	for( var i = 0 ; i < hidden_el_animation_details.length ; i++ )
	{
		cur_value_ranges = hidden_el_animation_details[i].value_ranges;
		
		for( var key in cur_value_ranges )
		{
			if( do_inverse == undefined || do_inverse == false )
			{
				start_val = cur_value_ranges[key].start_val;
				end_val = cur_value_ranges[key].end_val;
			}
			else
			{
				start_val = cur_value_ranges[key].end_val;
				end_val = cur_value_ranges[key].start_val;
			}
			
			this.all_animation_values[key] = this.get_value_on_animation_state(start_val, end_val, animation_state);
		}
		
		el.css(hidden_el_animation_details[i].style_prop_name, hidden_el_animation_details[i].prop_val(this.all_animation_values));
	}
};

custom_isotope.prototype.set_tween = function(from_memory_id, to_memory_id, animation_state) {
	var _this = this;
	
	TweenLite.to(animation_state,
		_this.options.tween_duration / 1000,
		{
			value: 1,
			ease: _this.options.tween_easing,
			onUpdate: _this.set_isotope_styles,
			onUpdateParams: [_this, from_memory_id, to_memory_id, animation_state],
			onComplete: _this.animation_on_complete,
			onCompleteParams: [_this, from_memory_id, to_memory_id]
		});
};

custom_isotope.prototype.set_transition = function(from_memory_id, to_memory_id, animation_state) {
	var _this = this,
	from_isotope_styles = _this.isotope_styles_memory[from_memory_id],
	to_isotope_styles = _this.isotope_styles_memory[to_memory_id],
	from_isotope_styles_values,
	to_isotope_styles_values,
	transition_css;
	
	_this.all_transition_completed = {};
	
	for( var key in from_isotope_styles )
	{
		from_isotope_styles_values = from_isotope_styles[key].values;
		to_isotope_styles_values = to_isotope_styles[key].values;
		
		_this[key].each(function(index, element) {
			var $this = $(this);
			
			if( from_isotope_styles_values[index] != 'hidden' && to_isotope_styles_values[index] != 'hidden' )
			{
				transition_css = '';
				
				for( var style_key in from_isotope_styles_values[index] )
				{
					if( transition_css != '' )
						transition_css += ', ';
					
					transition_css += style_key + ' ' + _this.transition_extra_css;
				}
				
				$this.css('transition', transition_css);
			}
			else
			{
				$this.css('transition', _this.hidden_el_transition_css);
			}
		});
		
		_this.set_transition_end_event(key, from_memory_id, to_memory_id);
	}
	
	animation_state.value = 1;
	
	setTimeout(function() {
		_this.set_isotope_styles(_this, from_memory_id, to_memory_id, animation_state);
	}, 10);
};

custom_isotope.prototype.set_transition_end_event = function(el_key, from_memory_id, to_memory_id) {
	var _this = this;
	
	_this.all_transition_completed[el_key] = 0;
	
	_this[el_key].on(transition_end_event_str, function(e) {
		if( e.target == e.currentTarget )
		{
			_this.all_transition_completed[el_key]++;
			_this.transition_completed(from_memory_id, to_memory_id);
		}
	});
};

custom_isotope.prototype.transition_completed = function(from_memory_id, to_memory_id) {
	var all_transition_completed = true;
	
	for( var key in this.isotope_styles_memory[from_memory_id] )
	{
		if( this.all_transition_completed[key] < this.all_total_isotope_styles_applied[key] )
		{
			all_transition_completed = false;
			break;
		}
	}
	
	if( all_transition_completed )
	{
		for( var key in this.isotope_styles_memory[from_memory_id] )
			this[key].off(transition_end_event_str);
		
		this.all_transition_completed = undefined;
		
		for( var key in this.isotope_styles_memory[from_memory_id] )
			this[key].css('transition', '');
		
		this.animation_on_complete(this, from_memory_id, to_memory_id);
	}
};

custom_isotope.prototype.animation_on_complete = function(_this, from_memory_id, to_memory_id) {
	_this.animation_running = false;
	
	_this.remove_all_isotope_styles(from_memory_id, to_memory_id);
	
	if( _this.options.auto_delete_all_memory_on_animation_complete )
	{
		for( var memory_id in _this.isotope_styles_memory )
			_this.delete_isotope_styles(memory_id);
	}
	
	_this.all_animation_values = undefined;
	
	_this.all_total_isotope_styles_applied = undefined;
	
	if( _this.on_animation_complete_callback != undefined )
		_this.on_animation_complete_callback();
};

custom_isotope.default_options = default_options;

custom_isotope.set_styles_methods = set_styles_methods;

})(jQuery);