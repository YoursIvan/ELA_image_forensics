   M S B A M L   `   `   `  ���� J  FWpfApplication1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfApplication1WpfApplication1       WpfApplication1.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     /localclr-namespace:WpfApplication1     -     �Name$	  ELA�� � Icon$- 'pack://siteoforigin:,,,/Icon/search.ico�� � Title$  ELA - Error Level Analysis��$��572.999��$��1366.158�����	#FFD1DDEA � 	Resources 2   ,WpfApplication1.EnabledToVisibilityConverter   etv��%   &	          .��� -    J� 	AllowDrop . ����  � ColumnDefinitions �� $
��256* ��� $
��256* ��� $
��171* �.+�o� -    o�Name$
 menu��$��3��$	��Top=���Z� .@�m� -    m�Name$ menuitem_file��$��	文件(F)��.@�m� -   $ button_open��$��  打开图片��$
��Left=�$	��149�� m� Icon �� $,��&pack://siteoforigin:,,,/Icon/inbox.ico��	 J� RenderTransformOrigin$	 0,-0.217&�$��20��m� -   $ button_save��$��  保存结果��{�.  $
��Left=�$	��149�� �� $+��%pack://siteoforigin:,,,/Icon/save.ico��$	 0,-0.217&�$��20���� m� -   $ button_quit��$��  退出��$
��Left=�$	��149�� �� $-��'pack://siteoforigin:,,,/Icon/logout.ico��$	 0,-0.217&�$��20��m� -   $ menuitem_analysis��$��	分析(A)��.@�m� -	   $ Button_R��$��  ELA��$
��Left=�{�.  $	��149�� �� $/��)pack://siteoforigin:,,,/Icon/start256.ico��$	 0,-0.217&�$��20��m� -
   $ menuitem_setting��$��	设置(S)��.@�m� -   $ screenshot_Menu��
 m� IsCheckable
 . $��  截图模式��$
��Left=�$	��149�� �� $1��+pack://siteoforigin:,,,/Icon/ScreenShot.ico��$	 0,-0.217&�$��20���� m� -   $ button_setting��$��  参数设置��$
��Left=�$	��149�� �� $+��%pack://siteoforigin:,,,/Icon/gear.ico��$	 0,-0.217&�$��20��m� -   $ menuitem_help��$��	帮助(H)��.@�m� -   $ button_help��$��  关于ELA��$
��Left=�$	��149�� �� $+��%pack://siteoforigin:,,,/Icon/info.ico��$	 0,-0.217&�$��20��c� -    c�Name$ toolBar��$��
0,21,-20,0q�$	 -1.607,0.499&�$��3��$��37��$	��Top=����	#FFE0DDD2$
��Left=�$
��1378��.@��� -    ��Name$ open_Toolbar�� � ToolTip$ 打开图片��.���� $,��&pack://siteoforigin:,,,/Icon/inbox.ico���� -   $ save_Toolbar��{�.  $ 保存结果��.���� -    ��Name$ save_Toolbar_img��$0��*pack://siteoforigin:,,,/Icon/save_grey.ico���� �� -   $ run_Toolbar��{�.  $ 	ELA分析��.���� -   $ run_Toolbar_img��$4��.pack://siteoforigin:,,,/Icon/start256_grey.ico��d� -    d�Name$ ScreenShot_button��$ 截图模式��.���� $1��+pack://siteoforigin:,,,/Icon/ScreenShot.ico���� -   .���� $+��%pack://siteoforigin:,,,/Icon/gear.ico��$ 参数设置���� �� -   .���� $+��%pack://siteoforigin:,,,/Icon/info.ico��$ 	关于ELA���� -   .���� $-��'pack://siteoforigin:,,,/Icon/logout.ico��$ 退出程序���� -    ��Name$ Box_org��$��Tamper Region Location��$��10,63,10,10q�$
��Bold"�$��14(�.���� -   $ 	image_org��$
��Fill=� .  � Cursor$
 None��{�.  $	��477��$	��445���� -   $ Box_ela��$��	ELA Image��$��10,63,10,10q�$��1��$
��Bold"�$��14(�.���� -   $ 	image_ela��$
��Fill=��� -   $ Parameter_setting��$��Parameter Setting��$��10,63,10,10q�$��2��$
��Bold"�$��14(�.��� $��2,0,-2,0q�.+��� -    ��Name$ label_Quality��$��	Quality: ��$��	10,10,0,0q�$
��Bold"�$��12(�$
��Left=�$��59��$��26��$	��Top=��� -   $ label_Threshold��$��
Threshold:��$��	10,92,0,0q�$
��Bold"�$��12(�$��26��$	��Top=�$
��Left=�$��73���� -    $ label_Blocksize��$��
Blocksize:��$��
10,172,0,0q�$
��Bold"�$��12(�$
��Left=�$��73��$��26��$	��Top=��� -!   $ label_Location��$��Location��$��
10,254,0,0q�$
��Bold"�$��12(�$
��Left=�$��62��$��26��$	��Top=��� -"   $ label_Denoise��$��Denoise��$��
10,328,0,0q�$
��Bold"�$��12(�$
��Left=�$��59��$��26��$	��Top=��� -#    ��Name$ Denoise_comboBox��$ Choose The Denoise Method��$��
32,359,0,0q� �� SelectedIndex$ 0��$ �� IsSynchronizedWithCurrentItem$
 TrueY�$��12(�$��Normal"�$
��Left=�$	��230��$��22��$	��Top=�.@��� $��None - ColorScale ELA��$��Normal"�$��12(�$
��Left=��� $��None - GrayScale ELA��$��Normal"�$��12(�$
��Left=��� $��Square Mean Denoise��$��Normal"�$��12(�$
��Left=��� $��Median Filter Denoise��$��Normal"�$��12(�$
��Left=��� $��Gaussian Filter Denoise��$��Normal"�$��12(�$
��Left=��� -$   $ Location_comboBox��$% Choose The Location Image Style��$��
32,285,0,0q�$ 0��$��12(�$��Normal"�$
��Left=�$	��230��$��22��$	��Top=�.@��� $"��Tamper Region Location(Dots)��$��Normal"�$��12(�$
��Left=��� $%��Tamper Region Location(Windows)���� $
 None��$��ELA Overlay Image��$��Normal"�$��12(��� $��
Test Image��$��Normal"�$��12(�$
��Left=��� -%    ��Name$ quality_slider��$��	32,41,0,0q� � Maximum$	 100Q� � SmallChange$ 1Q� � LargeChange$ 1Q�$��26��$	��Top=�$
��Left=�$	��230���� -&   $ threshold_slider��$��
32,123,0,0q�$	 100Q�$
��Left=�$	��230��$��26��$	��Top=�$	 100Q�$ 1Q� � Minimum$ 10Q��� -'   $ blocksize_slider��$��
32,203,0,0q�$ 1Q�$ 4Q�$ 24Q�$
��Left=�$	��230��$��26��$	��Top=��� -(   $ label��$��4��$��
10,203,0,0q�$��Normal"�$��12(�$��26��$	��Top=�$
��Left=�$��17���� -)   $ label1��$��24��$��262,203,0,0q�$��Normal"�$��12(�$��26��$	��Top=�$
��Left=�$��24���� -*   $ label2��$	��1.0��$��	4,123,0,0q�$��Normal"�$��12(�$
��Left=�$��27��$��26��$	��Top=��� -+   $ label3��$
��10.0��$��257,123,0,0q�$��Normal"�$��12(�$
��Left=�$��34��$��26��$	��Top=��� -,   $ label4��$��0��$��	10,41,0,0q�$��Normal"�$��12(�$
��Left=�$��17��$��26��$	��Top=��� --   $ label5��$	��100��$��
257,41,0,0q�$��Normal"�$��12(�$
��Left=�$��31��$��26��$	��Top=��� -.   $ Tamper_poss��$��Tamper Possibility：��$
��Left=�$��
10,396,0,0q�$	��Top=�$��12(��� -/    ��Name$ 
qualityBox��$( "Quality Must Be A Integer In 0~100�� �� 	MaxLength$ 3��$
��Left=�$��20��$��	69,13,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$��39��$��12(�#��� ��y���  �� Path$ 	IsEnabled� �� ElementName$ quality_slider��  �� NotifyOnValidationError  . ! �� 	Converter#! [  ���� $ Value�$ quality_slider��" �� UpdateSourceTrigger$" PropertyChanged=�# �� Mode$# TwoWay=�$ �� StringFormat$$ {0:F0}���� -0   $ thresholdBox��$- 'Threshold Must Be A Decimal In 1.0~10.0��$ 4��$
��Left=�$��20��$��	83,95,0,0q�$
 Wrap=�$	��Top=�$��49��$��12(�#��� ��y��� $ 	IsEnabled�$ threshold_slider��#! [  ���� *Value+$ threshold_slider��$# TwoWay=�$$ {0:F1}��$" PropertyChanged=��� -1   $ blocksizeBox��$) #Blocksize Must Be A Integer In 4~24��$ 2��$
��Left=�$��20��$��
83,175,0,0q�$
 Wrap=�$	��Top=�$��39��$��12(�#��� ��y��� $ 	IsEnabled�$ blocksize_slider��#! [  ���� *Value+$ blocksize_slider��$# TwoWay=�$$ {0:F0}��$" PropertyChanged=�