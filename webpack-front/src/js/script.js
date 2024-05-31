
$(function(){


	/* ---- Countdown timer ---- */

	$('#counter').countdown({
		timestamp : (new Date(2024,5,30,18,0,0)).getTime() + 8*10*60*60*1000
	});


	/* ---- Animations ---- */

	$('#links a').on('mouseenter', function() {
		$(this).animate({ left: 3 }, 'fast');
		}).on('mouseleave', function() {
		$(this).animate({ left: 0 }, 'fast');
		});
		$('footer a').on('mouseenter', function() {
			$(this).animate({ top: 3 }, 'fast');
		}).on('mouseleave', function() {
			$(this).animate({ top: 0 }, 'fast');
		});




});
