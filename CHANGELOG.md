## [1.0.0] 2024-07-06
### First Release
## [1.0.1] 2024-07-10
### Meta files patch
## [1.1.0] 2024-07-10
### Now it is not required to define UI type in code.
Assign one of the next values:
 - PopAbove
 - OpenNewWindow

In UiType filed on view to define if opened Ui element should hide all previous views or only pop above them
## [1.1.1] 2024-07-10
### Minor fixes for UI View setup
## [1.1.2] 2024-07-10
### Added setting for UI parent transform for BaseUiViewManager
## [1.1.3] 2024-07-11
### Fixed Sequence contains no elements exception when opening first UI view in list
## [1.1.4] 2024-07-11
### Fixed sprite selection by SpriteContainerBinding if property value is null
## [1.1.5] 2024-07-11
### Fixed ui state handling
## [1.1.6] 2024-07-12
### Fixed view scale and position on open
## [1.1.7] 2024-07-12
### Minor implementation changes
## [1.2.0] 2024-10-18
### Changes to BaseUiViewManager
 - Replaced ResourceBasePath with more flexible ResourceSearchPattern with default value "Prefabs/UI/{UiViewType}/{UiViewType}" where {UiViewType} will be automatically replaced with string Enum value of UiView
 - Made GetResourcesUiPath method virtual to allow custom path generation
 - Full expanding each UiView RectTransform on every open - now every UiView will fully cover UiViewsContainer every time
## [1.2.1] 2024-10-18
### Improvements to ResourceSearchPattern parsing
## [1.2.2] 2024-10-19
### Now it is possible to open UIView with specific Context State
### Added method to validate last opened view
## [1.3.0] 2024-11-21
### Added MasterPathBinding. Now it is possible to specify same parent context for al child bindings not to write context name at the beginning several times