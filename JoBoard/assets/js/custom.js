jQuery(function($) {

	'use strict';
	
	$(".loader").delay(1000).fadeOut("slow");
	$("#overlayer").delay(1000).fadeOut("slow");	



	var sitePlusMinus = function() {
		$('.js-btn-minus').on('click', function(e){
			e.preventDefault();
			if ( $(this).closest('.input-group').find('.form-control').val() != 0  ) {
				$(this).closest('.input-group').find('.form-control').val(parseInt($(this).closest('.input-group').find('.form-control').val()) - 1);
			} else {
				$(this).closest('.input-group').find('.form-control').val(parseInt(0));
			}
		});
		$('.js-btn-plus').on('click', function(e){
			e.preventDefault();
			$(this).closest('.input-group').find('.form-control').val(parseInt($(this).closest('.input-group').find('.form-control').val()) + 1);
		});
	};
	// sitePlusMinus();

   var siteIstotope = function() {
  	/* activate jquery isotope */
	  var $container = $('#posts').isotope({
	    itemSelector : '.item',
	    isFitWidth: true
	  });

	  $(window).resize(function(){
	    $container.isotope({
	      columnWidth: '.col-sm-3'
	    });
	  });
	  
	  $container.isotope({ filter: '*' });

	    // filter items on button click
	  $('#filters').on( 'click', 'button', function(e) {
	  	e.preventDefault();
	    var filterValue = $(this).attr('data-filter');
	    $container.isotope({ filter: filterValue });
	    $('#filters button').removeClass('active');
	    $(this).addClass('active');
	  });
  }

  siteIstotope();

  var fancyBoxInit = function() {
	  $('.fancybox').on('click', function() {
		  var visibleLinks = $('.fancybox');

		  $.fancybox.open( visibleLinks, {}, visibleLinks.index( this ) );

		  return false;
		});
	}
	fancyBoxInit();


	var stickyFillInit = function() {
		$(window).on('resize orientationchange', function() {
	    recalc();
	  }).resize();

	  function recalc() {
	  	if ( $('.jm-sticky-top').length > 0 ) {
		    var elements = $('.jm-sticky-top');
		    Stickyfill.add(elements);
	    }
	  }
	}
	stickyFillInit();


	// navigation

  var counterInit = function() {
		if ( $('.section-counter').length > 0 ) {
			$('.section-counter').waypoint( function( direction ) {

				if( direction === 'down' && !$(this.element).hasClass('ftco-animated') ) {

					var comma_separator_number_step = $.animateNumber.numberStepFactories.separator(',')
					$('.number').each(function(){
						var $this = $(this),
							num = $this.data('number');
							console.log(num);
						$this.animateNumber(
						  {
						    number: num,
						    numberStep: comma_separator_number_step
						  }, 7000
						);
					});
					
				}

			} , { offset: '95%' } );
		}

	}
	counterInit();

	var selectPickerInit = function() {
		$('.selectpicker').selectpicker();
	}
	selectPickerInit();

	var owlCarouselFunction = function() {
		$('.single-carousel').owlCarousel({
	    loop:true,
	    margin:0,
	    nav:true,
	    autoplay: true,
	    items:1,
	    nav: false,
	    smartSpeed: 1000
		});

	}
	owlCarouselFunction();

	var quillInit = function() {

		var toolbarOptions = [
		  ['bold', 'italic', 'underline', 'strike'],        // toggled buttons
		  ['blockquote', 'code-block'],

		  [{ 'header': 1 }, { 'header': 2 }],               // custom button values
		  [{ 'list': 'ordered'}, { 'list': 'bullet' }],
		  [{ 'script': 'sub'}, { 'script': 'super' }],      // superscript/subscript
		  [{ 'indent': '-1'}, { 'indent': '+1' }],          // outdent/indent
		  [{ 'direction': 'rtl' }],                         // text direction

		  [{ 'size': ['small', false, 'large', 'huge'] }],  // custom dropdown
		  [{ 'header': [1, 2, 3, 4, 5, 6, false] }],

		  [{ 'color': [] }, { 'background': [] }],          // dropdown with defaults from theme
		  [{ 'font': [] }],
		  [{ 'align': [] }],

		  ['clean']                                         // remove formatting button
		];

		if ( $('.editor').length > 0 ) {
			var quill = new Quill('#editor-1', {
			  modules: {
			    toolbar: toolbarOptions,
			  },
			  placeholder: 'Compose an epic...',
			  theme: 'snow'  // or 'bubble'
			});
			var quill = new Quill('#editor-2', {
			  modules: {
			    toolbar: toolbarOptions,
			  },
			  placeholder: 'Compose an epic...',
			  theme: 'snow'  // or 'bubble'
			});
		}

	}
	quillInit();
  
});