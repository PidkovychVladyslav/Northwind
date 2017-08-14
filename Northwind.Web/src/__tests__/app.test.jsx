import React from 'react';
import { mount } from 'enzyme';
import app from 'app';

describe('app', () => {
	var React = require('react');
	var TestUtils = require('react-addons-test-utils');
	var Accordion;

	beforeEach(function () {
		Table = require('react-table');
	});

	it('should exists', function () {
		// Render into document
		var table = TestUtils.renderIntoDocument(<ReactTable />);
		expect(TestUtils.isCompositeComponent(table)).toBeTruthy();
	});
});
